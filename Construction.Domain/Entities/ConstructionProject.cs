using Construction.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Construction.Domain.Entities
{
    public class ConstructionProject
    {
        [Key]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Project Name is required")]
        [StringLength(200, ErrorMessage = "Project Name can't be longer than 200 characters")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Project Location is required")]
        [StringLength(500, ErrorMessage = "Project Location can't be longer than 500 characters")]
        public string ProjectLocation { get; set; }

        [Required(ErrorMessage = "Project Stage is required")]
        [AllowedNames("Concept", "Design & Documentation", "Pre-Construction", "Construction")]
        public string ProjectStage { get; set; }

        [Required(ErrorMessage = "Project Category is required")]
        public string ProjectCategory { get; set; }

        [Required(ErrorMessage = "Project Start Date is required")]
        public DateTime ProjectConstructionStartDate { get; set; }

        [Required(ErrorMessage = "Project Detail is required")]
        [StringLength(2000, ErrorMessage = "Project Detail can't be longer than 2000 characters")]
        public string ProjectDetail { get; set; }

        [Required(ErrorMessage = "Project Creator is required")]
        public string ProjectCreatorID { get; set; }

    }
}
