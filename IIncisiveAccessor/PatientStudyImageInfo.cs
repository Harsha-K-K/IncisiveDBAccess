using ChDefine;
using PatientDataInfo;
using System.Runtime.Serialization;

namespace IIncisiveAccessor
{
    [DataContract]
    public class PatientStudyImageInfo
    {
        [DataMember]
        public ImageSeriesInfo imageSeriesInfo { get; private set; }

        [DataMember]
        public PatientInfo patientInfo { get; private set; }

        [DataMember]
        public StudyInfo studyInfo { get; private set; }

        public PatientStudyImageInfo(ImageSeriesInfo imageSeriesInfo, PatientInfo patientInfo, StudyInfo studyInfo)
        {
            this.imageSeriesInfo = imageSeriesInfo;
            this.patientInfo = patientInfo;
            this.studyInfo = studyInfo;
        }
    }
}
