using ConnpassAutomator.Domain.Model.Connpass;
using ConnpassAutomator.Domain.Model.Connpass.Event;
using ConnpassAutomator.Domain.Model.Profile;
using ConnpassAutomator.Domain.Model.Selenium;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;

namespace ConnpassAutomator.Application.Service
{
    public class ConnpassEventService
    {
        private readonly ISeleniumRepository seleniumRepository;

        public CreateEventResultState CreateEvent(Project project, Credential credential)
        {
            var driver = seleniumRepository.CreateWebDriver(120);
            driver.Url = ConnpassUrl.eventManagementUrl();

            var driverWait = seleniumRepository.CreateWait(driver, 60, 1);

            var result = DoPageOperation(driver, driverWait, project, credential);

            driver.Close();

            return result;
        }

        private CreateEventResultState DoPageOperation(WebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait driverWait, Project project, Credential credential)
        {
            if (!TryWith(() => Login(driver, credential)))
                return CreateEventResultState.ログイン失敗;

            if (!TryWith(() => FindBaseEventAndCopy(driver, project)))
                return CreateEventResultState.失敗;

            if (!TryWith(() => EditEvent(driver, project.Changeset)))
                return CreateEventResultState.失敗;

            if (!TryWith(() => PublishImmediately(driver, driverWait)))
                return CreateEventResultState.失敗;

            return CreateEventResultState.成功;
        }

        private void Login(WebDriver driver, Credential credential)
        {
            driver.InputText("username", credential.UserName);
            driver.InputText("password", credential.Password);
            driver.FindElement(By.Id("login_form")).Submit();

            //TODO:さて
            Thread.Sleep(1000);

            if (driver.GetClassTextOf("title_3_bg") != "イベント管理")
                throw new Exception("ログイン後画面のタイトルが見つからない。");
        }

        private void FindBaseEventAndCopy(WebDriver driver, Project project)
        {
            var elements = driver.FindElements(By.ClassName("event_list"));
            foreach (var element in elements)
            {
                //label_status_event mb_5 close 終了
                var status = element.FindElement(By.ClassName("label_status_event")).Text;

                //C#によるマルチコアのための非同期/並列処理プログラミング Zoomオンライン読書会 vol.5;
                var title = element.FindElement(By.CssSelector(".title > a")).Text;
                if (title.Contains(project.CopySource.EventTitle) && status == "終了")
                {
                    element.FindElement(By.ClassName("icon_gray_copy")).Click();
                    break;
                }
            }
            //コピー作成。
            var alert = driver.SwitchTo().Alert();
            alert.Accept();

            //TODO:さて
            Thread.Sleep(1000);

            //「イベントをコピーしました」がスライドインする
            var messageArea = driver.FindElement(By.Id("flash_message_area"));
            messageArea.Click();
            Thread.Sleep(400);

            if (driver.GetClassTextOf("public_status_area") != "下書き中")
                throw new Exception("イベントコピー後の「下書き中」が見つからない。");
        }

        private void EditEvent(WebDriver driver, Changeset changeset)
        {
            //タイトル編集
            EditTextPartOfEvent(driver, "FieldTitle", "title", changeset.EventTitle);
            //サブタイトル編集
            EditTextPartOfEvent(driver, "FieldSubTitle", "sub_title", changeset.SubEventTitle);
            //開催日時編集
            EditDateAndTimeOfEvent(driver, changeset);
            //イベント編集
            EditTextPartOfEvent(driver, "FieldDescription", "description_input", changeset.Explanation);
        }

        private void EditTextPartOfEvent(WebDriver driver, string titleElementId, string valueElementName, string text)
        {
            var fieldTitle = driver.FindElement(By.Id(titleElementId));
            //編集モードに入る
            fieldTitle.Click();
            //中身の文字
            var title = fieldTitle.FindElement(By.Name(valueElementName));
            //書き換える
            var titleValue = title.GetAttribute("value");
            title.Clear();
            title.SendKeys(text);

            var submit = fieldTitle.FindElement(By.CssSelector("button[type=submit]"));
            submit.Click();
        }

        private void EditDateAndTimeOfEvent(WebDriver driver, Changeset changeset)
        {
            var fieldTitle = driver.FindElement(By.Id("EventDates"));
            //編集モードに入る
            fieldTitle.Click();

            var startDate = EditDateOrTimePickerOnEvent(fieldTitle, "start_date", changeset.StartDate);
            EditDateOrTimePickerOnEvent(fieldTitle, "start_time", changeset.StartTime);
            EditDateOrTimePickerOnEvent(fieldTitle, "end_date", changeset.EndDate);
            EditDateOrTimePickerOnEvent(fieldTitle, "end_time", changeset.EndTime);

            //ピッカーが保存ボタンに被ると、ボタンが押せなくなる
            //ピッカーを消すために、開始日時をクリックする
            startDate.Click();

            var submit = fieldTitle.FindElement(By.CssSelector("button[type=submit]"));
            submit.Click();
        }

        private IWebElement EditDateOrTimePickerOnEvent(IWebElement area, string pickerElementName, string text)
        {
            var picerArea = area.FindElement(By.Name(pickerElementName));
            picerArea.Clear();
            picerArea.SendKeys(text);
            picerArea.SendKeys("\t");
            return picerArea;
        }

        private void PublishImmediately(WebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait driverWait)
        {
            //即時公開する
            driver.ClickClassOf("PublishEvent");
            driver.ClickClassOf("PopupSubmit");

            //公開されたことを確認して終了
            System.Diagnostics.Debug.WriteLine(driver.Url);
            driverWait.Until((_) => driver.Url.Contains("published"));


            if (driver.GetClassTextOf("main_title_2") != "イベントを公開しました")
                throw new Exception("イベント公開後の「公開しました」文言が見つからない。");
        }

        private bool TryWith(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public ConnpassEventService(ISeleniumRepository seleniumRepository)
            => this.seleniumRepository = seleniumRepository;
    }
}
