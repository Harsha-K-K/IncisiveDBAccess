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

            return seriesInstanceUids.ToEnumerable();
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetSopInstanceUids(string seriesInstanceUids)
        {
            var sopInstanceUids = new StringList();

            pOle.GetSOPInstanceUIDLinkOfImageSeries(seriesInstanceUids, ref sopInstanceUids);

            return sopInstanceUids.ToEnumerable();
        }

        /// <inheritdoc/>
        public PatientInfo GetPatientInfo(string studyInstanceUids)
        {
            var patientInfo = new PatientInfo();

            pOle.GetPatientInfo(studyInstanceUids, ref patientInfo);

            return patientInfo;
        }

        /// <inheritdoc/>
        public StudyInfo GetStudyInfo(string studyInstanceUids)
        {
            var studyInfo = new StudyInfo();

            pOle.GetStudyInfo(studyInstanceUids, ref studyInfo);

            return studyInfo;
        }

        /// <inheritdoc/>
        public ImageSeriesInfo GetImageSeriesInfo(string seriesInstanceUids)
        {
            var imageSeriesInfo = new ImageSeriesInfo();

            pOle.GetImageSeriesInfo(seriesInstanceUids, ref imageSeriesInfo);

            return imageSeriesInfo;
        }

        /// <inheritdoc/>
        public IEnumerable<PatientStudyInfo> GetAllPatientStudyInfo()
        {
            var patientStudyInfoList = new PatientStudyInfoList();

            pOle.GetPatientStudyInfoList(ref patientStudyInfoList);

            return patientStudyInfoList.ToEnumerable();
        }

        /// <inheritdoc/>
        public PatientStudyInfo GetPatientStudyInfo(string studyInstanceUids)
        {
            var patientStudyInfo = new PatientStudyInfo();

            pOle.GetPatientStudyInfo(studyInstanceUids, ref patientStudyInfo);

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

            return patientStudyImageInfo;

        }
    }
}