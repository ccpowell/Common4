using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Services
{
    public class Image : File
    {
        protected const int MAX_WIDTH = 348;
        protected const int MAX_HEIGHT = 480;

        public String AlternateText { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }

        #region Constructors
        
        public Image()
        {
            // Initialize defaults
            Init(MAX_HEIGHT, MAX_WIDTH);
        }

        public Image(int maxHeight, int maxWidth)
        {
            Init(maxHeight, maxWidth);
        }

        public Image(int maxHeight, int maxWidth, string altText)
        {
            Init(maxHeight, maxWidth);
            AlternateText = altText;
        }

        #endregion

        #region Protected Members

        protected void Init(int maxHeight, int maxWidth)
        {
            this.MaxWidth = maxWidth;
            this.MaxHeight = maxHeight;
        }

        #endregion
    }
}
