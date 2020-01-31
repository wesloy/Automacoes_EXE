using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bradesco.Fraude.VO.ResponseSmsVO
{
    public class ResponseSmsDeserializable
    {
        public string category { get; set; }

        public class ChatMessage
        {
            public string id { get; set; }
            public string message { get; set; }
            public DateTime createdAt { get; set; }
            public int sender { get; set; }
        }

        public class Attempt
        {
            public string id { get; set; }
            public int number { get; set; }
            public int timeout { get; set; }
        }

        public class Category
        {
            public List<object> fields { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string message { get; set; }
            public string reference { get; set; }
            public bool active { get; set; }
            public bool effectiveness { get; set; }
            public object attendanceTypeForwarding { get; set; }
            public object attendanceTypeScheduleTimeMinutes { get; set; }
        }

        public class Dictionary
        {
            public string id { get; set; }
            public string answers { get; set; }
            public bool active { get; set; }
        }

        public class Type
        {
            public string id { get; set; }
            public string value { get; set; }
            public Dictionary dictionary { get; set; }
        }

        public class ResponseFlow
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public bool manual { get; set; }
            public string acknowledgmentMessage { get; set; }
            public List<Category> categories { get; set; }
            public List<object> expectedFields { get; set; }
            public Type type { get; set; }
        }

        public class AttendanceType
        {
            public string id { get; set; }
            public string name { get; set; }
            public object acronym { get; set; }
            public string message { get; set; }
            public string extraCardMessage { get; set; }
            public string reference { get; set; }
            public bool active { get; set; }
            public string incorrectAnswerMessage { get; set; }
            public List<Attempt> attempts { get; set; }
            public List<ResponseFlow> responseFlows { get; set; }
        }

        public class GroupAttendanceTypePriority
        {
            public string id { get; set; }
            public int priority { get; set; }
            public AttendanceType attendanceType { get; set; }
        }

        public class Type2
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool regex { get; set; }
            public bool options { get; set; }
            public bool isGrouped { get; set; }
            public bool isPhone { get; set; }
            public bool isDate { get; set; }
            public bool isDateHour { get; set; }
            public bool isIdentity { get; set; }
            public bool isNumber { get; set; }
            public bool isSelect { get; set; }
            public bool isString { get; set; }
            public bool isExtraCard { get; set; }
        }

        public class AttendanceField
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool required { get; set; }
            public bool encryptedDb { get; set; }
            public bool encryptedView { get; set; }
            public bool showWhenClosing { get; set; }
            public string regex { get; set; }
            public bool priority { get; set; }
            public string options { get; set; }
            public string placeholder { get; set; }
            public Type2 type { get; set; }
            public bool isPhone { get; set; }
            public bool isExtraCard { get; set; }
        }

        public class Attempt2
        {
            public string id { get; set; }
            public int number { get; set; }
            public int timeout { get; set; }
        }

        public class Category2
        {
            public List<object> fields { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string message { get; set; }
            public string reference { get; set; }
            public bool active { get; set; }
            public bool effectiveness { get; set; }
            public object attendanceTypeForwarding { get; set; }
            public object attendanceTypeScheduleTimeMinutes { get; set; }
        }

        public class Dictionary2
        {
            public string id { get; set; }
            public string answers { get; set; }
            public bool active { get; set; }
        }

        public class Type3
        {
            public string id { get; set; }
            public string value { get; set; }
            public Dictionary2 dictionary { get; set; }
        }

        public class ResponseFlow2
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public bool manual { get; set; }
            public string acknowledgmentMessage { get; set; }
            public List<Category2> categories { get; set; }
            public List<object> expectedFields { get; set; }
            public Type3 type { get; set; }
        }

        public class AttendanceType2
        {
            public string id { get; set; }
            public string name { get; set; }
            public object acronym { get; set; }
            public string message { get; set; }
            public string extraCardMessage { get; set; }
            public string reference { get; set; }
            public bool active { get; set; }
            public string incorrectAnswerMessage { get; set; }
            public List<Attempt2> attempts { get; set; }
            public List<ResponseFlow2> responseFlows { get; set; }
        }

        public class Broker
        {
            public string id { get; set; }
            public string name { get; set; }
            public string domain { get; set; }
            public int maxMessageLength { get; set; }
            public bool token { get; set; }
            public bool appId { get; set; }
            public bool login { get; set; }
            public bool password { get; set; }
        }

        public class BrokerConnection
        {
            public string id { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public object token { get; set; }
            public object appId { get; set; }
            public double pricePerMessage { get; set; }
            public Broker broker { get; set; }
        }

        public class ResponseType
        {
            public string id { get; set; }
            public string value { get; set; }
        }

        public class Dictionary3
        {
            public string id { get; set; }
            public string answers { get; set; }
            public bool active { get; set; }
            public ResponseType responseType { get; set; }
        }

        public class Operation
        {
            public List<AttendanceField> attendanceFields { get; set; }
            public List<AttendanceType2> attendanceTypes { get; set; }
            public BrokerConnection brokerConnection { get; set; }
            public List<Dictionary3> dictionaries { get; set; }
            public List<object> operationFieldPriorities { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public int attendanceClosingLifetime { get; set; }
            public int communication { get; set; }
            public bool hasDefaultDictionaries { get; set; }
        }

        public class Role
        {
            public List<object> users { get; set; }
            public string description { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class RolePermission
        {
            public string id { get; set; }
            public bool read { get; set; }
            public bool write { get; set; }
            public bool delete { get; set; }
            public Role role { get; set; }
        }

        public class Profile
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public List<RolePermission> rolePermissions { get; set; }
        }

        public class Group
        {
            public string id { get; set; }
            public string name { get; set; }
            public object description { get; set; }
            public bool active { get; set; }
            public List<GroupAttendanceTypePriority> groupAttendanceTypePriorities { get; set; }
            public Operation operation { get; set; }
            public Profile profile { get; set; }
            public List<string> userIds { get; set; }
        }

        public class LockedBy
        {
            public List<object> claims { get; set; }
            public List<Group> groups { get; set; }
            public List<object> logins { get; set; }
            public List<object> roles { get; set; }
            public string id { get; set; }
            public bool externalUser { get; set; }
            public bool superUser { get; set; }
            public bool active { get; set; }
            public string signature { get; set; }
            public bool firstAccess { get; set; }
            public DateTime timeToChangePassword { get; set; }
            public bool requiredChangePass60Days { get; set; }
            public object email { get; set; }
            public bool emailConfirmed { get; set; }
            public string passwordHash { get; set; }
            public string securityStamp { get; set; }
            public object phoneNumber { get; set; }
            public bool phoneNumberConfirmed { get; set; }
            public bool twoFactorEnabled { get; set; }
            public object lockoutEndDateUtc { get; set; }
            public bool lockoutEnabled { get; set; }
            public int accessFailedCount { get; set; }
            public string userName { get; set; }
        }

        public class Category3
        {
            public List<object> fields { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string message { get; set; }
            public string reference { get; set; }
            public bool active { get; set; }
            public bool effectiveness { get; set; }
            public object attendanceTypeForwarding { get; set; }
            public object attendanceTypeScheduleTimeMinutes { get; set; }
        }

        public class Dictionary4
        {
            public string id { get; set; }
            public string answers { get; set; }
            public bool active { get; set; }
        }

        public class Type4
        {
            public string id { get; set; }
            public string value { get; set; }
            public Dictionary4 dictionary { get; set; }
        }

        public class ResponseFlow3
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public bool manual { get; set; }
            public string acknowledgmentMessage { get; set; }
            public List<Category3> categories { get; set; }
            public List<object> expectedFields { get; set; }
            public Type4 type { get; set; }
        }

        public class Type5
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool regex { get; set; }
            public bool options { get; set; }
            public bool isGrouped { get; set; }
            public bool isPhone { get; set; }
            public bool isDate { get; set; }
            public bool isDateHour { get; set; }
            public bool isIdentity { get; set; }
            public bool isNumber { get; set; }
            public bool isSelect { get; set; }
            public bool isString { get; set; }
            public bool isExtraCard { get; set; }
        }

        public class Field
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool required { get; set; }
            public bool encryptedDb { get; set; }
            public bool encryptedView { get; set; }
            public bool showWhenClosing { get; set; }
            public string regex { get; set; }
            public bool priority { get; set; }
            public string options { get; set; }
            public string placeholder { get; set; }
            public Type5 type { get; set; }
            public bool isPhone { get; set; }
            public bool isExtraCard { get; set; }
        }

        public class AttendanceFieldsValue
        {
            public Field field { get; set; }
            public string id { get; set; }
            public string value { get; set; }
            public object groupId { get; set; }
            public int groupNumber { get; set; }
        }

        public class RootObject
        {
            public object category { get; set; }
            public List<ChatMessage> chatMessages { get; set; }
            public object closedBy { get; set; }
            public List<object> fields { get; set; }
            public LockedBy lockedBy { get; set; }
            public ResponseFlow3 responseFlow { get; set; }
            public string id { get; set; }
            public object closingTime { get; set; }
            public bool isLocked { get; set; }
            public bool isClosed { get; set; }
            public string lifetimeTaskId { get; set; }
            public List<AttendanceFieldsValue> attendanceFieldsValues { get; set; }
            public object attendanceParent { get; set; }
        }
    }
}
