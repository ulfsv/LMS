select users.Email, users.UserName, users.FirstName, users.LastName, roles.Name as Role
from AspNetUsers as users
join AspNetUserRoles as userroles
on users.Id = userroles.UserId
join AspNetRoles as roles
on userroles.RoleId = roles.Id
where roles.Name = 'TEACHER'






TiffanyMuller21@gmail.com

12#¤qwER