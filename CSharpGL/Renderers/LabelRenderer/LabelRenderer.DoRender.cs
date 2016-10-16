﻿namespace CSharpGL
{
    /// <summary>
    /// Renders a label that always faces camera in 3D space.
    /// </summary>
    public partial class LabelRenderer
    {
        private long billboardCenter_worldspaceTicks;
        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        protected override void DoRender(RenderEventArgs arg)
        {
            {
                long tick = this.WorldPosition.UpdateTicks;
                if (tick != this.billboardCenter_worldspaceTicks)
                {
                    this.SetUniform("billboardCenter_worldspace", this.WorldPosition.Value);
                    this.billboardCenter_worldspaceTicks = tick;
                }
            }

            if (labelHeightRecord.IsMarked())
            {
                this.SetUniform("labelHeight", this.LabelHeight);
                labelHeightRecord.CancelMark();
            }
            if (textRecord.IsMarked())
            {
                if (this.Model != null)
                {
                    (this.Model as TextModel).SetText(this.text, this.fontTexture);
                }
            }
            if (discardTransparencyRecord.IsMarked())
            {
                bool discard = this.DiscardTransparency;
                this.SetUniform("discardTransparency", discard);
                this.blendSwitch.Enabled = discard;
                discardTransparencyRecord.CancelMark();
            }
            int[] viewport = OpenGL.GetViewport();
            this.SetUniform("viewportSize", new vec2(viewport[2], viewport[3]));
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            this.SetUniform("projection", projection);
            this.SetUniform("view", view);

            base.DoRender(arg);
        }
    }
}