using PatientDataInfo;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IncisiveDBAccessor.Extensions
{
    [CollectionDataContract]
    [KnownType(typeof(PatientStudyInfo))]
    static class ListExtensions
    {
        /// <summary>
        /// Converts StringList to Enumerable type
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToEnumerable(this StringList strings)
        {
            for( var item=0; item< strings.Count; item++) 
            {
                yield return strings.GetAt(item);
            }
        }

        /// <summary>
        /// Converts PatientStudyInfoList to Enumerable type
        /// </summary>
        /// <param name="patientStudyInfos"></param>
        /// <returns></returns>
        public static IEnumerable<PatientStudyInfo> ToEnumerable(this PatientStudyInfoList patientStudyInfos)
        {
            for (var item = 0; item < patientStudyInfos.Count; item++)
            {
                 yield return patientStudyInfos.GetAt(item);
            }
        }
    }
}
