using MVCKutuphane.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCKutuphane.Models.Sınıflarım
{
    public class VitrinViewModel
    {
        public IPagedList<TBLKITAP> Kitaplar { get; set; }

        public List<TBLHAKKIMIZDA> Hakkimizda { get; set; }
    }
}