using ConnpassAutomator.Domain.Model.Connpass.Event;
using ConnpassAutomator.Domain.Model.Profile;
using ConnpassAutomator.Domain.Model.Selenium;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace ConnpassAutomator.Application.Service
{
    public class ConnpassEventService
    {
        private readonly ISeleniumRepository seleniumRepository;

        public CreateEventResultState CreateEvent(Project project, Credential credential)
        {
            var driver = seleniumRepository.CreateWebDriver(120);
            driver.Url = "https://connpass.com/editmanage/";

            var driverWait = seleniumRepository.CreateWait(driver, 60, 1);

            driver.FindElement(By.Name("username")).SendKeys(credential.UserName);
            driver.FindElement(By.Name("password")).SendKeys(credential.Password);
            driver.FindElement(By.Id("login_form")).Submit();

            //TODO:さて
            Thread.Sleep(1000);

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

            //タイトル編集
            {
                var fieldTitle = driver.FindElement(By.Id("FieldTitle"));
                //編集モードに入る
                fieldTitle.Click();
                //中身の文字
                var title = fieldTitle.FindElement(By.Name("title"));
                //タイトルを書き換える
                var titleValue = title.GetAttribute("value");
                title.Clear();
                title.SendKeys(project.Changeset.EventTitle);

                var submit = fieldTitle.FindElement(By.CssSelector("button[type=submit]"));
                submit.Click();
            }
            //サブタイトル編集
            {
                var fieldTitle = driver.FindElement(By.Id("FieldSubTitle"));
                //編集モードに入る
                fieldTitle.Click();
                //中身の文字
                var title = fieldTitle.FindElement(By.Name("sub_title"));
                //タイトルを書き換える
                var titleValue = title.GetAttribute("value");
                title.Clear();
                title.SendKeys(project.Changeset.SubEventTitle);

                var submit = fieldTitle.FindElement(By.CssSelector("button[type=submit]"));
                submit.Click();
            }
            //開催日時編集
            {
                var fieldTitle = driver.FindElement(By.Id("EventDates"));
                //編集モードに入る
                fieldTitle.Click();

                //ピッカーが出るのを消すために\tを投げる
                //中身の文字
                var startDate = fieldTitle.FindElement(By.Name("start_date"));
                startDate.Clear();
                startDate.SendKeys(project.Changeset.StartDate);
                startDate.SendKeys("\t");

                //中身の文字
                var startTime = fieldTitle.FindElement(By.Name("start_time"));
                startTime.Clear();
                startTime.SendKeys(project.Changeset.StartTime);
                startTime.SendKeys("\t");

                var endDate = fieldTitle.FindElement(By.Name("end_date"));
                endDate.Clear();
                endDate.SendKeys(project.Changeset.EndDate);
                endDate.SendKeys("\t");

                //中身の文字
                var endTime = fieldTitle.FindElement(By.Name("end_time"));
                endTime.Clear();
                endTime.SendKeys(project.Changeset.EndTime);
                endTime.SendKeys("\t");

                //ピッカーが保存ボタンに被ると、ボタンが押せなくなる
                //ピッカーを消すために、開始日時をクリックする
                startDate.Click();

                var submit = fieldTitle.FindElement(By.CssSelector("button[type=submit]"));
                submit.Click();
            }
            //イベント編集
            {
                var fieldTitle = driver.FindElement(By.Id("FieldDescription"));
                //編集モードに入る
                fieldTitle.Click();
                //中身の文字
                var title = fieldTitle.FindElement(By.Name("description_input"));
                //タイトルを書き換える
                var titleValue = title.GetAttribute("value");
                title.Clear();
                title.SendKeys(project.Changeset.Explanation);
                var submit = fieldTitle.FindElement(By.CssSelector("button[type=submit]"));
                submit.Click();
            }


            //即時公開する
            {
                var publishEvent = driver.FindElement(By.ClassName("PublishEvent"));
                publishEvent.Click();

                var popupSubmit = driver.FindElement(By.ClassName("PopupSubmit"));
                popupSubmit.Click();
            }

            //公開されたことを確認して終了
            System.Diagnostics.Debug.WriteLine(driver.Url);
            driverWait.Until((_) => driver.Url.Contains("published"));
            var mainTitle = driver.FindElement(By.ClassName("main_title_2"));
            if (mainTitle.Text != "イベントを公開しました")
            {
                throw new Exception("NG!!");
            }

            driver.Close();
            driver = null;

            return CreateEventResultState.成功;
        }

        public ConnpassEventService(ISeleniumRepository seleniumRepository)
            => this.seleniumRepository = seleniumRepository;
    }
}
