using System.Reflection;

namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetColumns
    {

        public FlowSheetColumns(string title, string field, int? order) {
            Title = title;
            Field = field;
            DisplayOrder = order;
        }
        public string Title { get; set; }

        public string Field { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
