namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class DashboardViewModel
    {
        public int ContactUsCount { get; set; }
        public int CareerCount { get; set; }
        public List<int> CareerMonthCount { get; set; }
        public List<int> ContactMonthCount { get; set; }
    }
}
