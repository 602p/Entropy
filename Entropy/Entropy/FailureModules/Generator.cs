﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ippo;
using UnityEngine;
using KSP;

namespace coffeeman
{
	public class ModuleGeneratorReliability : ippo.FailureModule
	{
		ModuleGenerator generator;

		public override string DebugName { get { return "ModuleGenerator"; } }
		public override string ScreenName { get { return "Generator"; } }
		public override string FailureMessage { get { return "A generator has aged, decreasing efficiency"; } }
		public override string RepairMessage { get { return "You have repaired the generator"; } }
		public override string FailGuiName { get { return "Fail generator"; } }
		public override string EvaRepairGuiName { get { return "Repair generator"; } }
		public override string MaintenanceString { get { return "Clean generator"; } }

		public override bool PartIsActive()
		{
			// It's active when its active (duh!)
			return generator.generatorIsActive;
		}

		protected override void DI_Start(StartState state)
		{
			generator = this.part.Modules.OfType<ModuleGenerator>().Single();
		}

		protected override bool DI_FailBegin()
		{
			return true;
		}

		protected override void DI_Disable()
		{
			generator.outputList.ForEach (r => r.rate /= 2);
		}


		protected override void DI_EvaRepair()
		{
			generator.outputList.ForEach (r => r.rate *= 2);
		}

		protected override void DI_Update(){
			if (this.HasFailed) {
				generator.efficiency /= 2;
			}
		}
	}
}

