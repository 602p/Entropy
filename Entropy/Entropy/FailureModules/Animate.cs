﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ippo;
using UnityEngine;
using KSP;

namespace coffeeman
{
	public class ModuleAnimationReliability : ippo.FailureModule
	{
		ModuleAnimateGeneric bay;

		public override string DebugName { get { return "ModuleAnimateGeneric"; } }
		public override string ScreenName { get { return "Motor"; } }
		public override string FailureMessage { get { return "A motor has failed"; } }
		public override string RepairMessage { get { return "You have repaired the motor"; } }
		public override string FailGuiName { get { return "Fail motor"; } }
		public override string EvaRepairGuiName { get { return "Repair motor"; } }
		public override string MaintenanceString { get { return "Lubricate motor"; } }
		public override string ExtraEditorInfo{ get { return "This part can become stuck if it fails"; } }

		public override bool PartIsActive()
		{
			// It's only active if its not on the ground
			return !part.vessel.LandedOrSplashed;
		}

		protected override void DI_Start(StartState state)
		{
			bay = this.part.Modules.OfType<ModuleAnimateGeneric>().Single();
		}

		protected override bool DI_FailBegin()
		{
			return true;
		}

		protected override void DI_Disable()
		{
			bay.allowManualControl = false;
			bay.enabled = false;

		}


		protected override void DI_EvaRepair()
		{
			bay.allowManualControl = true;
			bay.enabled = true; 
		}
	}
}

