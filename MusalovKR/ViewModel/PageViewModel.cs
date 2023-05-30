using Common;

namespace MusalovKR.ViewModel
{
    public class PageViewModel
    {
        public int Index { get; set; }
        public bool CanShowNext { get; set; }
        public bool CanShowPrevious { get; set; }
        public Posts Post { get; set; }
    }
}
