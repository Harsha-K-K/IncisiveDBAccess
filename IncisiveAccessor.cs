using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ChDefine;
using IIncisiveAccessor;
using IncisiveDBAccessor.Extensions;
using PatientDataInfo;
using PatientDataService;

namespace IncisiveDBAccessor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class IncisiveAccessor : IIncisiveAccessor.IIncisiveAccessor
    {
        public PatientDataOLE pOle;

        public IncisiveAccessor()
        {
            pOle = PatientDataOLE.Instance;
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetImageFilePaths(IEnumerable<string> studyInstanceUids,
            IEnumerable<string> seriesInstanceUids, IEnumerable<string> sopInstanceUids)
        {
            IList<string> filepaths = new List<string>();

            pOle.GetImageFilePath(studyInstanceUids.ToList(), seriesInstanceUids.ToList(), sopInstanceUids.ToList(),
                ref filepaths);

            var filepath = string.Join(Environment.NewLine, filepaths);
            Console.WriteLine(filepath);

            return filepaths;
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetSeriesInstanceUids(string studyInstaceUid)
        {
            var seriesInstanceUids = new StringList();

            pOle.GetSeriesInstanceUIDLinkOfStudy(studyInstaceUid, ref seriesInstanceUids);

            var seriesInstanceUid = string.Join(Environment.NewLine, "SeriesInstanceUids: " + seriesInstanceUids);
            Console.WriteLine(seriesInstanceUid);

            return seriesInstanceUids.ToEnumerable();
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetSopInstanceUids(string seriesInstanceUids)
        {
            var sopInstanceUids = new StringList();

            pOle.GetSOPInstanceUIDLinkOfImageSeries(seriesInstanceUids, ref sopInstanceUids);

            var sopInstanceUid = string.Join(Environment.NewLine, "SopInstanceUids: " + sopInstanceUids);
            Console.WriteLine(sopInstanceUid);

            return sopInstanceUids.ToEnumerable();
        }

        /// <inheritdoc/>
        public PatientInfo GetPatientInfo(string studyInstanceUids)
        {
            var patientInfo = new PatientInfo();

            pOle.GetPatientInfo(studyInstanceUids, ref patientInfo);

            Console.WriteLine("GetPatientInfo");

            return patientInfo;
        }

        /// <inheritdoc/>
        public StudyInfo GetStudyInfo(string studyInstanceUids)
        {
            var studyInfo = new StudyInfo();

            pOle.GetStudyInfo(studyInstanceUids, ref studyInfo);

            Console.WriteLine("GetStudyInfo");

            return studyInfo;
        }

        /// <inheritdoc/>
        public ImageSeriesInfo GetImageSeriesInfo(string seriesInstanceUids)
        {
            var imageSeriesInfo = new ImageSeriesInfo();

            pOle.GetImageSeriesInfo(seriesInstanceUids, ref imageSeriesInfo);

            Console.WriteLine("GetImageSeriesInfo");

            return imageSeriesInfo;
        }

        /// <inheritdoc/>
        public IEnumerable<PatientStudyInfo> GetAllPatientStudyInfo()
        {
            const string sqlQuery = "OPEN SYMMETRIC KEY ConsoleSymmetric DECRYPTION BY CERTIFICATE ConsoleCert;" +
                "\r\nbegin try\r\nSelect top 5 * FROM StudyInfo, V_Pat_Reg_Info" +
                " where StudyInfo.StudyInstanceUID =V_Pat_Reg_Info.StudyInstanceUID order by StudyInfo.StudyTime desc" +
                "\r\nCLOSE SYMMETRIC KEY ConsoleSymmetric;\r\nend try\r\nbegin catch\r\n    CLOSE SYMMETRIC KEY ConsoleSymmetric;\r\nend catch";

            var patientStudyInfoList = new PatientStudyInfoList();

            pOle.GetPatientStudyInfoList(sqlQuery, ref patientStudyInfoList);

            Console.WriteLine("GetAllPatientStudyInfo");

            return patientStudyInfoList.ToEnumerable();
        }

        /// <inheritdoc/>
        public PatientStudyInfo GetPatientStudyInfo(string studyInstanceUids)
        {
            var patientStudyInfo = new PatientStudyInfo();

            pOle.GetPatientStudyInfo(studyInstanceUids, ref patientStudyInfo);

            Console.WriteLine("GetPatientStudyInfo");

            return patientStudyInfo;
        }

        /// <inheritdoc/>
        public PatientStudyImageInfo GetPatientStudyImageInfo(string studyInstanceUid)
        {
            var patientStudyInfo = new PatientStudyInfo();
            pOle.GetPatientStudyInfo(studyInstanceUid, ref patientStudyInfo);

            var seriesInstanceUids = new StringList();
            pOle.GetSeriesInstanceUIDLinkOfStudy(studyInstanceUid, ref seriesInstanceUids);

            var imageSeriesInfo = new ImageSeriesInfo();
            pOle.GetImageSeriesInfo(seriesInstanceUids.GetAt(0), ref imageSeriesInfo);

            var patientStudyImageInfo = new PatientStudyImageInfo(imageSeriesInfo, patientStudyInfo.PatientInfos[0], patientStudyInfo.StudyInfos[0]);

            Console.WriteLine("GetPatientStudyImageInfo");

            return patientStudyImageInfo;

        }
    }
}