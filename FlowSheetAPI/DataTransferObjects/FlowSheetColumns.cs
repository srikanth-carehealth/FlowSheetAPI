using System.Reflection;

namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetColumns
    {

        public FlowSheetColumns(string title, string field) {
            Title = title;
            Field = field;
        }
        public string Title { get; set; }

        public string Field { get; set; }
    }
}
