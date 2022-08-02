using Microsoft.Graph;

namespace OrganizationTest.Model
{
    public class UserDeptModel
    {
        public string _dept;
        public List<User> _user;
        public List<UserDeptModel> _child;

        public UserDeptModel()
        {
            _dept = "";
            _user = new();
            _child = new();
        }
        public UserDeptModel(string dept, List<User> user, List<UserDeptModel> child)
        {
            _dept = dept;
            _user = user;
            _child = child;
        }
        public UserDeptModel(string dept, User user)
        {
            _dept = dept;
            _user = new()
            {
                user
            };
            _child = new();
        }
        public UserDeptModel(string dept)
        {
            _dept = dept;
            _user = new();
            _child = new();
        }

        public static void SetChild(List<UserDeptModel> parents, User user, int index)
        {
            string[] depts = user.OfficeLocation.Split("/");
            if (depts.Length <= 1)
            {
                return;
            }

            var userDept = parents.Find(x => x._dept == depts[index]);
            if (userDept == null)
            {
                parents.Add(new(depts[0]));
                userDept = parents.Find(x => x._dept == depts[index]);
            }

            SetChild(userDept, user, index + 1);
        }
        public static void SetChild(UserDeptModel parents, User user, int index)
        {
            string[] depts = user.OfficeLocation.Split("/");
            if (depts.Length <= 1)
            {
                return;
            }

            var childDept = parents._child.Find(x => x._dept == depts[index]);
            if (childDept == null)
            {
                parents._child.Add(new(depts[index]));
                childDept = parents._child.Find(x => x._dept == depts[index]);
            }

            if (depts.Length - 1 == index)
            {
                childDept._user.Add(user);
            }
            else
            {
                SetChild(childDept, user, index + 1);
            }
        }
    }
}
