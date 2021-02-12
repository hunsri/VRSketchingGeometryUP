﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry.SketchObjectManagement;

namespace VRSketchingGeometry.Commands.Patch {
    /// <summary>
    /// Delete the last segment of the patch.
    /// </summary>
    public class DeleteSegmentCommand : ICommand
    {
        private PatchSketchObject PatchSketchObject;
        private List<Vector3> OldSegment;

        /// <summary>
        /// Delete the last segment of the patch sketch object.
        /// </summary>
        /// <param name="patchSketchObject"></param>
        public DeleteSegmentCommand(PatchSketchObject patchSketchObject) {
            this.PatchSketchObject = patchSketchObject;
        }

        public bool Execute()
        {
            OldSegment = this.PatchSketchObject.GetLastSegment();
            this.PatchSketchObject.RemovePatchSegment();
            return true;
        }

        public void Redo()
        {
            this.Execute();
        }

        public void Undo()
        {
            this.PatchSketchObject.AddPatchSegment(OldSegment);
        }
    }
}
