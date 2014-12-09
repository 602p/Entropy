﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using ippo;
using UnityEngine;
using KSP;

namespace coffeeman
{
	public class ModuleSRBFXReliability : ippo.FailureModule
	{
		ModuleEnginesFX srb;

		public override string DebugName { get { return "O-Ring"; } }
		public override string ScreenName { get { return "O-Ring"; } }
		public override string FailureMessage { get { return "An O-Ring has cracked, Abort!"; } }
		public override string RepairMessage { get { return ""; } }
		public override string FailGuiName { get { return "Fail O-Ring"; } }
		public override string EvaRepairGuiName { get { return ""; } }
		public override string MaintenanceString { get { return "Replace O-Ring"; } }
		public override string ExtraEditorInfo{ get { return "This part can overheat, overthrust and explode if it fails"; } }

		public bool overloading = false;
		public float overloadbonus = 0F;

		public override bool PartIsActive()
		{
			// A SRB is active if it is running
			return srb.EngineIgnited & srb.CalculateThrust()>0;
		}

		protected override void DI_Start(StartState state)
		{
			srb = this.part.Modules.OfType<ModuleEnginesFX>().Single();
		}

		protected override bool DI_FailBegin()
		{
			return true;
		}

		protected override void DI_Disable()
		{
			srb.heatProduction += srb.part.maxTemp/4F; //Increase the heat so it explodes
			overloading = true;
		}

		protected override void DI_EvaRepair(){}

		protected override void DI_Update(){
			if (overloading){
				srb.rigidbody.AddRelativeForce(Vector3.forward * overloadbonus); //Increase thrust thru hack
				overloadbonus += srb.maxThrust / 60; //This is a considerable amount
				if (!PartIsActive ()) {
					overloading = false; //Stop if the part is disabled
				}
			}
		}
	}
}

