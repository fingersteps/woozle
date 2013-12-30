//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Ef.Repository
{
    public partial class PermissionRepository  : AbstractRepository<Permission>
    {
    
    	public PermissionRepository(IEfUnitOfWork Context) : base(Context)
    	{
    	}
    
    
    	 public override Permission Synchronize(Permission entity, Session session) 
    	 { 
    		try
    		{
    			var stopwatch = new Stopwatch();
    			var attachedObj = Context.SynchronizeObject(entity, session);
    			
    			
    			//Navigation Property 'FunctionPermissions'
    			stopwatch.Start();
    			foreach(var n in entity.FunctionPermissions.Where(n => n.PersistanceState == PState.Added))
    			{ 
    				if (!attachedObj.FunctionPermissions.Contains(n)) attachedObj.FunctionPermissions.Add(n);
    				if (n is IMandatorCapable)
    				{
    					n.MandatorId = session.SessionObject.Mandator.Id;
    				}
    			} 
    			foreach(var n in entity.FunctionPermissions.Where(n => n.PersistanceState == PState.Modified || n.PersistanceState == PState.Deleted))
    			{ 
    					Context.SynchronizeObject(n, session); 
    			} 
    			stopwatch.Stop();
    			this.Logger.Info(string.Format("Synchronize state of '{0}', took {1} ms", "FunctionPermissions", stopwatch.ElapsedMilliseconds));
    			return attachedObj; 
    		}
    	catch (Exception e)
    	{
    		this.Logger.Error(e.Message); 
    		throw new PersistenceException(PersistenceOperation.SYNCHRONIZE, e); 
    	} 
      } 
    	 public override void Delete(Permission entity, Session session) 
    	 { 
    		try
    		{
    			var stopwatch = new Stopwatch();
    			entity.PersistanceState = PState.Unchanged;
    			var attachedObj = Context.SynchronizeObject(entity, session);
    			
    			
    
    			//Navigation Property 'FunctionPermissions'
    			stopwatch.Start();
    			Context.LoadCollection<Permission>(attachedObj.Id, "FunctionPermissions");
    			foreach (var n in attachedObj.FunctionPermissions.ToList())
    			{
    				n.PersistanceState = PState.Deleted;
    			    Context.SynchronizeObject(n, session);
    			} 
    			stopwatch.Stop();
    			this.Logger.Info(string.Format("Synchronize state of '{0}', took {1} ms", "FunctionPermissions", stopwatch.ElapsedMilliseconds));
    			attachedObj.PersistanceState = PState.Deleted;
    			attachedObj = Context.SynchronizeObject(attachedObj, session);
    			stopwatch.Start();
    			Context.Commit();
    			stopwatch.Stop();
    			this.Logger.Info(string.Format("Commit '{0}' Delete, took {1} ms", "Permission", stopwatch.ElapsedMilliseconds));
    		}
    	catch (Exception e)
    	{
    		this.Logger.Error(e.Message); 
    		throw new PersistenceException(PersistenceOperation.DELETE, e);  
    	} 
      } 
    
    }
    
}
