using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakan_project.models
{
    public class Colleges
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Universities")]
        public int Unversityid { get; set; }

        public virtual Universities Universities { get; set; }
    }
}
