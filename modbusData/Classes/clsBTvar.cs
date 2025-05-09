using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniproject.Classes
{
    internal class clsBTvar
    {
        internal double F1_InPer;
        internal double aggregate_Kg;
        internal double F3_InPerSum;
        internal double aggregate_KgSum;

        public int ID { get; set; }
        public string RegNo { get; set; }
        public string PlantCode { get; set; }
        public string OprLat { get; set; }
        public string OprLong { get; set; }
        public string OprJurisdiction { get; set; }
        public string OprDivision { get; set; }
        public string OprWorkName { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public double AggregateTPH { get; set; }
        public double BitumenKgMin { get; set; }
        public double FillerKgMin { get; set; }
        public double BitumenPer { get; set; }
        public double FillerPer { get; set; }
        public double F1InPer { get; set; }
        public double F2InPer { get; set; }
        public double F3InPer { get; set; }
        public double F4InPer { get; set; }
        public double Moisture { get; set; }
        public double MixTemp { get; set; }
        public double ExhaustTemp { get; set; }
        public double Tank1 { get; set; }
        public double Tank2 { get; set; }
        public string Tipper { get; set; }
        public double AggregateTon { get; set; }
        public double BitumenKg { get; set; }
        public double FillerKg { get; set; }
        public double NetMix { get; set; }
        public string BatchNo { get; set; }
        public string SrNo { get; set; }
        public Double BatchDurationInS { get; set; }
        public double WeightKgPerBatch { get; set; }
        public double HB1KgPerBatch { get; set; }
        public double HB2KgPerBatch { get; set; }
        public double HB3KgPerBatch { get; set; }
        public double HB4KgPerBatch { get; set; }
        public double AggregateKg { get; set; }
        public int TruckCount { get; set; }
        public string IMEINo { get; set; }
        public string WorkType { get; set; }
        public string WorkCode { get; set; }
        public string Material { get; set; }
        public string ExportStatus { get; set; }
        public string VIPLUpload { get; set; }
        public int BatchEndFlag { get; set; }
        public int LoadNo { get; set; }
        public string JobSite { get; set; }
        public string InsertType { get; set; }
        public double F2_InPer { get; internal set; }
        public double F3_InPer { get; internal set; }
        public double F4_InPer { get; internal set; }
        public double HB1_KgPerBatch { get; internal set; }
        public double HB2_KgPerBatch { get; internal set; }
        public double HB3_KgPerBatch { get; internal set; }
        public double HB4_KgPerBatch { get; internal set; }
        public double AggregateTPH_Sum { get; internal set; }
        public double BitumenKgMinSum { get; internal set; }
        public double FillerKgMinSum { get; internal set; }
        public double FillerPerSum { get; internal set; }
        public double BitumenPerSum { get; internal set; }
        public double F1_InPerSum { get; internal set; }
        public double F2_InPerSum { get; internal set; }
        public double F4_InPerSum { get; internal set; }
        public double MoistureSum { get; internal set; }
        public double ExhaustTempSum { get; internal set; }
        public double MixTempSum { get; internal set; }
        public double Tank1Sum { get; internal set; }
        public double Tank2Sum { get; internal set; }
        public double AggregateTonSum { get; internal set; }
        public double BitumenKgSum { get; internal set; }
        public double FillerKgSum { get; internal set; }
        public double NetMixSum { get; internal set; }
        public double BatchDurationInSSum { get; internal set; }
        public double WeightKgPerBatchSum { get; internal set; }
        public double HB1_KgPerBatchSum { get; internal set; }
        public double HB2_KgPerBatchSum { get; internal set; }
        public double HB3_KgPerBatchSum { get; internal set; }
        public double HB4_KgPerBatchSum { get; internal set; }

        //Recipe Master values.
        public string RecipeName { get; set; }
        public string RCP_F1 { get; set; }
        public string RCP_F2 { get; set; }
        public string RCP_F3 { get; set; }
        public string RCP_F4 { get; set; }
        public string RCP_Filler { get; set; }
        public string RCP_Bitumen { get; set; }


    }
}
