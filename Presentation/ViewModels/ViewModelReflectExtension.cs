using ConnpassAutomator.Domain.Model.Profile;

namespace Presentation.ViewModels
{
    static class ViewModelReflectExtension
    {
        internal static void ReflectTo(this MainWindowViewModel vm, Project project)
        {
            var changeSet = project.Changeset;

            project.CopySource.EventTitle = vm.CopyBaseEventTitle;
            changeSet.EventTitle = vm.EventTitle;
            changeSet.SubEventTitle = vm.SubTitle;
            changeSet.Explanation = vm.EventDescription;

            changeSet.StartDate = PickerValueConverter.ToDateStringOf(vm.StartDate);
            changeSet.StartTime = PickerValueConverter.ToTimeStringOf(vm.StartTime);
            changeSet.EndDate = PickerValueConverter.ToDateStringOf(vm.EndDate);
            changeSet.EndTime = PickerValueConverter.ToTimeStringOf(vm.EndTime);
        }

        internal static void ReflectFrom(this MainWindowViewModel vm, Project project)
        {
            var changeSet = project.Changeset;

            vm.CopyBaseEventTitle = project.CopySource.EventTitle;
            vm.EventTitle = changeSet.EventTitle;
            vm.SubTitle = changeSet.SubEventTitle;
            vm.EventDescription = changeSet.Explanation;

            vm.StartDate = PickerValueConverter.ToDatePickerValueOf(changeSet.StartDate);
            vm.StartTime = PickerValueConverter.ToTimePickerValueOf(changeSet.StartTime);
            vm.EndDate = PickerValueConverter.ToDatePickerValueOf(changeSet.EndDate);
            vm.EndTime = PickerValueConverter.ToTimePickerValueOf(changeSet.EndTime);
        }
    }
}
