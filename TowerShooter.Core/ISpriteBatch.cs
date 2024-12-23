using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;

namespace TowerShooter
{
    public interface ISpriteBatch
    {
        void Begin();
        void End();
        void LoadContent();
        void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix? transformMatrix);

        //
        // Summary:
        //     Submit a sprite for drawing in the current batch.
        //
        // Parameters:
        //   texture:
        //     A texture.
        //
        //   destinationRectangle:
        //     The drawing bounds on screen.
        //
        //   color:
        //     A color mask.
        void Draw(Texture2D texture, Rectangle destinationRectangle, Color color);
        //
        // Summary:
        //     Submit a sprite for drawing in the current batch.
        //
        // Parameters:
        //   texture:
        //     A texture.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   color:
        //     A color mask.
        void Draw(Texture2D texture, Vector2 position, Color color);
        //
        // Summary:
        //     Submit a sprite for drawing in the current batch.
        //
        // Parameters:
        //   texture:
        //     A texture.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   sourceRectangle:
        //     An optional region on the texture which will be rendered. If null - draws full
        //     texture.
        //
        //   color:
        //     A color mask.
        void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color);
        //
        // Summary:
        //     Submit a sprite for drawing in the current batch.
        //
        // Parameters:
        //   texture:
        //     A texture.
        //
        //   destinationRectangle:
        //     The drawing bounds on screen.
        //
        //   sourceRectangle:
        //     An optional region on the texture which will be rendered. If null - draws full
        //     texture.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this sprite.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this sprite.
        void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth);
        //
        // Summary:
        //     Submit a sprite for drawing in the current batch.
        //
        // Parameters:
        //   texture:
        //     A texture.
        //
        //   destinationRectangle:
        //     The drawing bounds on screen.
        //
        //   sourceRectangle:
        //     An optional region on the texture which will be rendered. If null - draws full
        //     texture.
        //
        //   color:
        //     A color mask.
        void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color);
        //
        // Summary:
        //     Submit a sprite for drawing in the current batch.
        //
        // Parameters:
        //   texture:
        //     A texture.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   sourceRectangle:
        //     An optional region on the texture which will be rendered. If null - draws full
        //     texture.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this sprite.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   scale:
        //     A scaling of this sprite.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this sprite.
        void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);
        //
        // Summary:
        //     Submit a sprite for drawing in the current batch.
        //
        // Parameters:
        //   texture:
        //     A texture.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   sourceRectangle:
        //     An optional region on the texture which will be rendered. If null - draws full
        //     texture.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this sprite.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   scale:
        //     A scaling of this sprite.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this sprite.
        void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);
        //
        // Summary:
        //     Submit a text string of sprites for drawing in the current batch.
        //
        // Parameters:
        //   spriteFont:
        //     A font.
        //
        //   text:
        //     The text which will be drawn.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this string.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   scale:
        //     A scaling of this string.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this string.
        void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);
        //
        // Summary:
        //     Submit a text string of sprites for drawing in the current batch.
        //
        // Parameters:
        //   spriteFont:
        //     A font.
        //
        //   text:
        //     The text which will be drawn.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   color:
        //     A color mask.
        void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color);
        //
        // Summary:
        //     Submit a text string of sprites for drawing in the current batch.
        //
        // Parameters:
        //   spriteFont:
        //     A font.
        //
        //   text:
        //     The text which will be drawn.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this string.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   scale:
        //     A scaling of this string.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this string.
        void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);
        //
        // Summary:
        //     Submit a text string of sprites for drawing in the current batch.
        //
        // Parameters:
        //   spriteFont:
        //     A font.
        //
        //   text:
        //     The text which will be drawn.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this string.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   scale:
        //     A scaling of this string.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this string.
        void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);
        //
        // Summary:
        //     Submit a text string of sprites for drawing in the current batch.
        //
        // Parameters:
        //   spriteFont:
        //     A font.
        //
        //   text:
        //     The text which will be drawn.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   color:
        //     A color mask.
        void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color);
        //
        // Summary:
        //     Submit a text string of sprites for drawing in the current batch.
        //
        // Parameters:
        //   spriteFont:
        //     A font.
        //
        //   text:
        //     The text which will be drawn.
        //
        //   position:
        //     The drawing location on screen.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this string.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   scale:
        //     A scaling of this string.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this string.
        void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);
        void BeginCameraZoom();
    }

}