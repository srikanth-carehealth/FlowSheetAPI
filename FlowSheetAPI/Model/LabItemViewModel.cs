using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.Model
{
    public class LabItemViewModel
    {
        public Guid LabItemId { get; set; }
        public string LabItemName { get; set; }
        public string? LabItemCode { get; set; }
        public bool IsActive { get; set; }
    }
}
