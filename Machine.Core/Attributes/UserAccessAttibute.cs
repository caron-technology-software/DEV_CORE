using System;

namespace Machine.Common
{
    public sealed class UserAccess : Attribute
    {
        public UserType ReadEnableUserType { get; set; }
        public UserType WriteEnableUserType { get; set; }

        public UserAccess(UserType readWriteUserType)
        {
            ReadEnableUserType = WriteEnableUserType = readWriteUserType;
        }

        public UserAccess(UserType readUserType, UserType writeUserType)
        {
            if (readUserType > writeUserType)
            {
                throw new ArgumentException();
            }

            ReadEnableUserType = readUserType;
            WriteEnableUserType = writeUserType;
        }
    }
}
