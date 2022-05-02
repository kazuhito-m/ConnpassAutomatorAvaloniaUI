namespace ConnpassAutomator.Domain.Model.Profile
{
    public class Project
    {
        public CopySource CopySource { get; set; } = new CopySource();
        public Changeset Changeset { get; set; } = new Changeset();

        public static Project DefaultWhenAddNew()
            => new()
            {
                CopySource = new CopySource()
                {
                    EventTitle = "未設定(入力・変更してください)"
                }
            };
    }
}
