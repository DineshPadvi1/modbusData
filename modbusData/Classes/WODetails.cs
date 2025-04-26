using System.Collections.Generic;

namespace Uniproject.Classes
{
    //public class WODetails
    //{
    //    public string plantRegNo { set; get; }
    //    public List<WorkOrders> allocatedWorkOrders { get; set; }
    //    public string woCode { set; get; }
    //    public string woName { set; get; }
    //    public string siteName { set; get; }        

    //}

    public class WODetails
    {
        public string plantRegNo { get; set; }
        public List<WorkOrder> allocatedWorkOrders { get; set; }

        public class WorkOrder
        {
            public string con_name { get; set; }
            public List<SiteName> site_names { get; set; }
            public string wo_code { get; set; }
            public string con_code { get; set; }
            public string wo_name { get; set; }
        }

        public class SiteName
        {
            public string site_name { get; set; }
        }
    }



}
