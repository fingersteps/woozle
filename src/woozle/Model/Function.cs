//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using Woozle.Core.Model;

namespace Woozle.Model
{
    [Serializable]
    public partial class Function : WoozleObject
    {
        public Function()
        {
            this.FunctionPermissions = new FixupCollection<FunctionPermission>();
        }
    
        private byte[] icon;
        private string name;
        private string description;
        private int moduleid;
        private string logicalid;
        private int translationid;
        private short sequence;
    
        public byte[] Icon 
    	{ 
    		get { return this.icon;} 
    		set { 
    			if(this.icon != value)
    			{
    				this.icon = value;
    				OnPropertyChanged("Icon");
    			}
    		}
    	}
        public string Name 
    	{ 
    		get { return this.name;} 
    		set { 
    			if(this.name != value)
    			{
    				this.name = value;
    				OnPropertyChanged("Name");
    			}
    		}
    	}
        public string Description 
    	{ 
    		get { return this.description;} 
    		set { 
    			if(this.description != value)
    			{
    				this.description = value;
    				OnPropertyChanged("Description");
    			}
    		}
    	}
        public int ModuleId 
    	{ 
    		get { return this.moduleid;} 
    		set { 
    			if(this.moduleid != value)
    			{
    				this.moduleid = value;
    				OnPropertyChanged("ModuleId");
    			}
    		}
    	}
        public string LogicalId 
    	{ 
    		get { return this.logicalid;} 
    		set { 
    			if(this.logicalid != value)
    			{
    				this.logicalid = value;
    				OnPropertyChanged("LogicalId");
    			}
    		}
    	}
        public int TranslationId 
    	{ 
    		get { return this.translationid;} 
    		set { 
    			if(this.translationid != value)
    			{
    				this.translationid = value;
    				OnPropertyChanged("TranslationId");
    			}
    		}
    	}
    	
    	/// <summary>
        /// To use the translated value directly it needs to be filled explicit
        /// </summary>
        public string TranslatedValue 	{ get; set; }
    
        public short Sequence 
    	{ 
    		get { return this.sequence;} 
    		set { 
    			if(this.sequence != value)
    			{
    				this.sequence = value;
    				OnPropertyChanged("Sequence");
    			}
    		}
    	}
    
    
    private FixupCollection<FunctionPermission> functionpermissions;
    
    public virtual FixupCollection<FunctionPermission> FunctionPermissions 
    { 
    	get { return functionpermissions; } 
    	set
    	{
    		functionpermissions = value;
    		if(value != null)
    		{
    			FunctionPermissions.CollectionChanged += OnCollectionChanged;
    		}
    	}
    }
    
    
    public virtual Module Module { get; set; }
    
    
    public virtual Translation Translation { get; set; }
    
    
    public event PropertyChangedEventHandler LocalPropertyChanged;
    private event EventHandler AnyPropertyChanged;
    
    protected void OnPropertyChanged(String propertyName)
    {
    	if (this.LocalPropertyChanged != null)
        {
            this.LocalPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
    	OnPropertyChanged("Collection");
    }
    
    public void HandleLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    	this.PersistanceState = PState.Modified;
    	AnyPropertyChanged(sender, e);
    }
    
    public void HandleAnyPropertyChanged(object sender, EventArgs e)
    {
    	this.Dirty = true;
    }
    
    public override void ActivatePropertyChangedEvent(bool resetPersistenceState)
    {
    	ActivatePropertyChangedEvent(resetPersistenceState, HandleAnyPropertyChanged);
    }
    
    public void ActivatePropertyChangedEvent(bool resetPersistenceState, EventHandler anyPropertyChangedHandler)
    {
    	if (this.LocalPropertyChanged == null && this.AnyPropertyChanged == null)
    	{
    		if(resetPersistenceState) 
    		{
    			this.PersistanceState = PState.Unchanged;
    			this.Dirty = false;
    		}
    
    		this.LocalPropertyChanged += HandleLocalPropertyChanged;
            this.AnyPropertyChanged += anyPropertyChangedHandler;
    		if(FunctionPermissions != null)
    		{
    			foreach(var n in FunctionPermissions) 
    			{
    				n.ActivatePropertyChangedEvent(resetPersistenceState, anyPropertyChangedHandler);
    			}
    		}
    	
    
    
    		if(Module != null)
    		{
    			Module.ActivatePropertyChangedEvent(resetPersistenceState, anyPropertyChangedHandler);
    		}
    
    
    		if(Translation != null)
    		{
    			Translation.ActivatePropertyChangedEvent(resetPersistenceState, anyPropertyChangedHandler);
    		}
    
    	}
    }
    
    public override void DeactivatePropertyChangedEvent()
    {
    	if(this.LocalPropertyChanged != null && this.AnyPropertyChanged != null) 
    	{
    		this.LocalPropertyChanged = null;
    		this.AnyPropertyChanged = null;
    		foreach(var n in FunctionPermissions) 
    		{
    			n.DeactivatePropertyChangedEvent();
    		}
    		if(Module != null)
    		{
    			Module.DeactivatePropertyChangedEvent();
    		}
    		if(Translation != null)
    		{
    			Translation.DeactivatePropertyChangedEvent();
    		}
    	}
    }
    
    public override bool Equals(object obj)
    {
        if (this == obj)
            return true;
        if (obj == null)
            return false;
        if (GetType() != obj.GetType())
            return false;
    	//objects are equal when they are not new (Id != 0) and the Ids are equal
        WoozleObject other = (WoozleObject)obj;
        if (Id == 0 || Id != other.Id)
            return false;
        return true;
    }
    
    public override int GetHashCode()
    {
        int prime = 31;
    	int result = 1;
    	result = prime * result + Id;
    	return result;
    }
    }
    
}