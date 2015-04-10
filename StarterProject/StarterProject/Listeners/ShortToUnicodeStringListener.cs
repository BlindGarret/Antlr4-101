using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StarterProject.Listeners
{
    class ShortToUnicodeStringListener : ArrayInitBaseListener
    {
        #region InitEnteredEvent

        public event InitEnteredEvent InitEntered;
        public delegate void InitEnteredEvent(EventArgs e);

        #endregion

        #region InitExitedEvent

        public event InitExitedEvent InitExited;
        public delegate void InitExitedEvent(EventArgs e);

        #endregion

        #region ValueEnteredEvent

        public event ValueEnteredEvent ValueEntered;
        public delegate void ValueEnteredEvent(string value,EventArgs e);

        #endregion

        public override void EnterInit(ArrayInitParser.InitContext context)
        {
            var local = InitEntered;
            if (local != null)
            {
                local(EventArgs.Empty);
            }
        }

        public override void ExitInit(ArrayInitParser.InitContext context)
        {
            var local = InitExited;
            if (local != null)
            {
                local(EventArgs.Empty);
            }
        }

        public override void EnterValue(ArrayInitParser.ValueContext context)
        {
            var number = context.INT();
            var translation = Convert.ToBase64String(Encoding.UTF8.GetBytes(number.ToString()));
            var local = ValueEntered;
            if (local != null)
            {
                local(translation, EventArgs.Empty);
            }
        }
    }
}
