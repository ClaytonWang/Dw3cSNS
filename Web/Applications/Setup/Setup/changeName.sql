select * from tn_InitialNavigations where NavigationId='10100202'

update tn_InitialNavigations set NavigationText=N'文章' where NavigationId='10100201'
update tn_InitialNavigations set NavigationText=N'文章首页' where NavigationId='10100202'
update tn_InitialNavigations set NavigationText=N'我的文章' where NavigationId='10100203'

update tn_InitialNavigations set NavigationText=N'文章' where NavigationId='11100201'
update tn_InitialNavigations set NavigationText=N'文章首页' where NavigationId='11100202'
update tn_InitialNavigations set NavigationText=N'我的文章' where NavigationId='11100203'

update tn_InitialNavigations set NavigationText=N'文章' where NavigationId='20100201'


select * from tn_ActivityItems
update tn_ActivityItems set ItemName=N'发布文章' where ItemKey='CreateBlogThread'
update tn_ActivityItems set ItemName=N'文章评论' where ItemKey='CreateBlogComment'

select * from tn_PermissionItems

update tn_PermissionItems set ItemName=N'发布文章' where ItemKey='Blog_Create'

select * from tn_AuditItems
update tn_AuditItems set ItemName=N'撰写文章' where ItemKey='Blog_Thread'

select * from tn_TenantTypes
update tn_TenantTypes set Name=N'文章应用' where TenantTypeId='100200'
update tn_TenantTypes set Name=N'文章' where TenantTypeId='100201'

select * from tn_Applications
update tn_Applications set Description=N'文章应用' where ApplicationId='1002'

select * from tn_ApplicationManagementOperations
update tn_ApplicationManagementOperations set OperationText=N'撰写文章' where OperationId='10100201'
update tn_ApplicationManagementOperations set OperationText=N'撰写文章' where OperationId='11100201'

select * from tn_Applications
update tn_Applications set IsLocked=0, IsEnabled=0

update tn_Applications set IsLocked=1 where ApplicationId=1002