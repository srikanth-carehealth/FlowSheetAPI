namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetColumns
    {
        public FlowSheetColumns(string columnId, string columnName, int? order)
        {
            Key = columnId; // Key Column_Id
            Value = columnName; // Value Column_Name
            DisplayOrder = order; // Value Column_display_Order
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
