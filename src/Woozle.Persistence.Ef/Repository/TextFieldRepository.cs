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
    public partial class TextFieldRepository  : AbstractRepository<TextField>
    {
    
    	public TextFieldRepository(IEfUnitOfWork Context) : base(Context)
    	{
    	}
    
    
    	 public override TextField Synchronize(TextField entity, SessionData sessionData) 
    	 { 
    		try
    		{
    			var stopwatch = new Stopwatch();
    			var attachedObj = Context.SynchronizeObject(entity, sessionData);
    			
    			attachedObj.Mandator = Context.SynchronizeObject(entity.Mandator, sessionData); 
    
    			attachedObj.Translation = Context.SynchronizeObject(entity.Translation, sessionData); 
    
    			attachedObj.Translation1 = Context.SynchronizeObject(entity.Translation1, sessionData); 
    
    			
    			return attachedObj; 
    		}
    		catch (Exception e)
    		{
    			Trace.TraceError(e.Message); 
    			throw new PersistenceException(PersistenceOperation.SYNCHRONIZE, e); 
    		} 
         } 
    
    }
    
}