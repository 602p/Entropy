using System;
using System.Collections;
using System.Linq;
using System.Text;
using ippo;
using UnityEngine;
using KSP;

namespace coffeeman
{
	public class ModuleSolarReliability : ippo.FailureModule
	{
		ModuleDeployableSolarPanel panel;

		public override string DebugName { get { return "Tracking Servo"; } }
		public override string ScreenName { get { return "Tracking Servo"; } }
		public override string FailureMessage { get { return "The sun-tracking servo in a solar panel has burnt out"; } }
		public override string RepairMessage { get { return "The servo has been repaired"; } }
		public override string FailGuiName { get { return "Fail Servo"; } }
		public override string EvaRepairGuiName { get { return "Replace Servo"; } }
		public override string MaintenanceString { get { return "Clean Servo"; } }
		public override string ExtraEditorInfo { get { return "This part's tracking motor can stop tracking if it fails"; } }

		public override bool PartIsActive()
		{
			// Panels are active if deployed AND TRACKING
			return panel.sunTracking & panel.flowRate>0;
		}

		protected override void DI_Start(StartState state)
		{
			panel = this.part.Modules.OfType<ModuleDeployableSolarPanel>().Single();
			if (!panel.sunTracking) {
				this.enabled = false; //Disable this if it's not tracking
			}
		}

		protected override bool DI_FailBegin()
		{
			return true;
		}

		protected override void DI_Disable()
		{
			panel.sunTracking = false;
		}

		protected override void DI_EvaRepair(){
			panel.sunTracking = true;
		}

		public override bool DI_ShowInfoInEditor(){
			return this.part.Modules.OfType<ModuleDeployableSolarPanel>().Single().sunTracking; //Don't show for non-tracking panels
		}
	}
}

