using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCVAPI.Models
{
	public class Skill : IId
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Time is required")]
		public string Time { get; set; }

		[Required(ErrorMessage = "Experience level is required")]
		public string ExperienceLevel { get; set; }
	}
}
