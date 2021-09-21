using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ProfileData
{
    //данные пользователя
    [Serializable]
    struct ProfileData
    {
        public string mail;
        public string phone;
        public string name;

        public ProfileData(string name, string mail, string phone)
        {
            this.name = name;
            this.mail = mail;
            this.phone = phone;
        }
    }

    static class Profile
    {
        private static ProfileData _profileData;
        public static ProfileData profileData => _profileData;
        public static void InitProfile(string name, string mail, string phone)
        {
            _profileData = new ProfileData(name, mail, phone);
        }
    }
}
