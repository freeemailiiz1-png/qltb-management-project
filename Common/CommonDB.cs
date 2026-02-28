using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThietBi.Common
{
    internal class CommonDB
    {
        public int STATUS_AVAIABLE = 1;
        public int STATUS_UNAVAIABLE = 0;
        public int STATUS_PAYMENT = 2;
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (phoneNumber.Length != 10)
                return false;
            foreach (var t in phoneNumber)
            {
                if (!char.IsDigit(t))
                    return false;
            }
            return true;
        }

        public bool IsValidCCCD(string cccd)
        {
            cccd = new string(cccd.Where(char.IsDigit).ToArray());
            if (cccd.Length != 12)
                return false;
            foreach (var t in cccd)
            {
                if (!char.IsDigit(t))
                    return false;
            }
            return true;
        }
        public void ValidateInputNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Nếu ký tự không phải là số hoặc Backspace, không cho phép nhập
            }
        }

        public void EnableButtons(bool status, params Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.Enabled = status;
            }
        }

        public void ResetForm(params TextBox[] textBoxes)
        {
            foreach (var textBoxe in textBoxes)
            {
                textBoxe.Text = "";
            }
        }
    }
}
