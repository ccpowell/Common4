using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Util;
using DRCOG.Common.Collections;

namespace DRCOG.Common.Domain
{
    [Serializable]
    public abstract class CommonEntity<IdT> : BaseEntity<IdT>, IVersionable<Byte[]>, IValidatable
    {
        #region IVersionable Members

        public virtual Byte[] Version { get; set; }

        public virtual string VersionToString()
        {
            if (Version != null)
            {
                return ByteArrayHelper.ToString(Version);
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region IValidatable Members

        private Dictionary<String, String> _validationResults;

        public abstract Boolean IsValid();

        protected void AddValidationResult(String key, String description)
        {
            if (_validationResults == null)
            {
                _validationResults = new Dictionary<String, String>();
            }
            _validationResults.Add(key, description);
        }

        public virtual IDictionary<String, String> GetValidationResults()
        {
            if (_validationResults == null)
            {
                _validationResults = new Dictionary<String, String>();
            }

            return new ReadOnlyDictionary<String, String>(_validationResults);
        }

        public virtual void ResetValidation()
        {
            _validationResults = null;
        }

        #endregion
    }
}
