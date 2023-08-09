using ChDefine;
using PatientDataInfo;
using System.Collections.Generic;
using System.ServiceModel;

namespace IIncisiveAccessor
{
    /// <summary>
    ///     Interface which communicates with Incisive Database
    /// </summary>
    [ServiceContract]
    public interface IIncisiveAccessor
    {
        /// <summary>
        ///     Gets the dicom image file path.
        /// </summary>
        /// <param name="studyInstanceUids">Study Instance UID</param>
        /// <param name="seriesInstanceUids">Series Instance UID</param>
        /// <param name="sopInstaceUids">SOP Instance UID</param>
        /// <returns>List of filepaths</returns>
        [OperationContract]
        IEnumerable<string> GetImageFilePaths(IEnumerable<string> studyInstanceUids,
            IEnumerable<string> seriesInstanceUids, IEnumerable<string> sopInstaceUids);

        /// <summary>
        ///     Gets all the series instance UID of the study.
        /// </summary>
        /// <param name="studyInstanceUid">Study Instance UID</param>
        /// <returns>List of series instance UIDs</returns>
        [OperationContract]
        IEnumerable<string> GetSeriesInstanceUids(string studyInstanceUid);

        /// <summary>
        ///     Gets all the SOP Instance UIDs corresponding to it's Series Instance UID
        /// </summary>
        /// <param name="seriesInstanceUid">Series Instance UID</param>
        /// <returns>List of SOP instance UIDs</returns>
        [OperationContract]
        IEnumerable<string> GetSopInstanceUids(string seriesInstanceUid);

        /// <summary>
        ///     Gets the patient info corresponding to the study instance UID
        /// </summary>
        /// <param name="studyInstanceUid"></param>
        /// <returns>Patient Info</returns>
        [OperationContract]
        PatientInfo GetPatientInfo(string studyInstanceUid);

        /// <summary>
        ///     Gets the study info corresponding to the study instance UID
        /// </summary>
        /// <param name="studyInstanceUid"></param>
        /// <returns>Study info</returns>
        [OperationContract]
        StudyInfo GetStudyInfo(string studyInstanceUid);

        /// <summary>
        ///     Gets the image series info corresponding to the Series Instance UID.
        /// </summary>
        /// <param name="seriesInstanceUid"></param>
        /// <returns>image series info</returns>
        [OperationContract]
        ImageSeriesInfo GetImageSeriesInfo(string seriesInstanceUid);

        /// <summary>
        ///     Get all the patient and study info present in database
        /// </summary>
        /// <returns>List of all the patients present</returns>
        [OperationContract]
        IEnumerable<PatientStudyInfo> GetAllPatientStudyInfo();

        /// <summary>
        ///     Gets the Patient and Study info corresponding to the study instance UID
        /// </summary>
        /// <param name="studyInstanceUid"></param>
        /// <returns>Patient Study info</returns>
        [OperationContract]
        PatientStudyInfo GetPatientStudyInfo(string studyInstanceUid);

        /// <summary>
        ///     Gets the Patient and Study and Image info to the study instance UID
        /// </summary>
        /// <param name="studyInstanceUid"></param>
        /// <returns>Patient Study info</returns>
        [OperationContract]
        PatientStudyImageInfo GetPatientStudyImageInfo(string studyInstanceUid);
    }
}
