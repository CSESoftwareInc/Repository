# CSESoftware.Repository

A generic repository pattern. 

Note: This defines the interfaces. For implimentations, please see CSESoftware.Repository.EntityFramework or CSESoftware.EntityFrameworkCore

---

## Instructions for using the Query Builder

All below queries assume the following class structure:

```c#
public class User : BaseEntity<Guid>
{
	public string Name { get; set; }
	public string AddressOne { get; set; }
	public string AddressTwo { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Zip { get; set; }
	public string Phone { get; set; }
	public string SecondaryPhone { get; set; }
	public string Initials { get; set; }
	public Department Department { get; set; }
}

public class Department : BaseEntity<Guid>
{
	public string Name { get; set; }
}
```
Basic query with a predicate:

```c#
public async Task Query()
{
	var inActiveUsersQuery = new QueryBuilder<User>()
		.Where(x => x.IsActive == false)
		.Build();

	var inActiveUsers = await repository.GetAllAsync(inActiveUsersQuery);
}
```
Query with a join to another entity:

```c#
public async Task Join()
{
	var inActiveUsersWithDepartmentsQuery = new QueryBuilder<User>()
		.Where(x => x.IsActive == false)
		.Include(x => x.Department)
		.Build();

	var inActiveUsersWithDepartments = await _repository.GetAllAsync(inActiveUsersWithDepartmentsQuery);
}
```
Pagination with skip/take:

```c#
public async Task Paginate()
{
	var paginateInActiveUsersQuery = new QueryBuilder<User>()
		.Where(x => x.IsActive == false)
		.Skip(5)
		.Take(10)
		.Build();

	var paginatedInActiveUsers = await _repository.GetAllAsync(paginateInActiveUsersQuery);
}
```
Ordering query results:

```c#
public async Task Order()
{
	var orderUsersQuery = new QueryBuilder<User>()
		.OrderBy(x => x.OrderBy(y => y.Name).ThenByDescending(y => y.AddressOne))
		.Build();

	var orderedUsers = await _repository.GetAllAsync(orderUsersQuery);
}
```
Specifying columns to be returned:

```c#
public async Task SpecifyReturn()
{
	var nameOfInactiveUsersQuery = new QueryBuilder<User>()
		.Where(x => x.IsActive == false)
		.Select(x => x.Name)
		.Build();

	var nameOfInactiveUsers = await _repository.GetAllAsync(nameOfInactiveUsersQuery);
}
```
Queries can be saved through concrete classes and reused later:

```c#
public class UserQueries
{
	public IQuery<User> GetAllInactiveUsers()
	{
		return new QueryBuilder<User>()
			.Where(x => x.IsActive == false)
			.Build();
	}

	public IQuery<User> GetAllInactiveUsers()
	{
		return new QueryBuilder<User>()
			.Where(x => x.IsActive == false)
			.Include(x => x.Department)
			.Build();
	}
}
```