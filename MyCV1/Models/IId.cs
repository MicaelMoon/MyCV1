﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCV1.Models
{
	public interface IId
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
