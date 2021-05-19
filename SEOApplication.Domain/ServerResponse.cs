using SEOApplication.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SEOApplication.Domain
{
    public class ServerResponse : IDisposable
    {
        public bool Result { get; set; }
        public Error ErrorMessages { get; set; }
        public object Data { get; set; }

        public Error Error { get; set; }

        #region Dispose
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //clear managed objects
            }

            //clear unmanaged objects
        }
        #endregion
    }
}
