using MyCaffe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCaffe.ViewModels
{
    public class ThumbnailBoxViewModel
    {
        public IEnumerable<ThumbnailModel> Thumbnails { get; set; }
    }
}