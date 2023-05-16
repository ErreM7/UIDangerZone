using System.Collections.Generic;
using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("PVPZone", "ErreM", 1.0)]
    [Description("Creación de una interfaz, la cual indique si se esta en zona PVP o no.")]
    public class UIDangerZone : RustPlugin
    {
        private Dictionary<ulong, bool> pvpStatus = new Dictionary<ulong, bool>();

        void OnTick()
        {
            foreach (BasePlayer player in BasePlayer.activePlayerList)
            {
                Vector3 position = player.transform.position;
                bool inPvpZone = IsInPvpZone(position);
                EstablecesEstatus(player.userID, inPvpZone);
                MostrarEstatus(player);
            }
        }

        bool IsInPvpZone(Vector3 position)
        {
            // Implementacion lógica para determinar si la posición dada corresponde a una zona de PVP o no.
            // Retorna true si el jugador se encuentra en una zona de PVP, o false si no.
            return false;
        }

        void EstablecesEstatus(ulong userId, bool inPvpZone)
        {
            pvpStatus[userId] = inPvpZone;
        }

        void MostrarEstatus(BasePlayer player)
        {
            bool inPvpZone = pvpStatus[player.userID];
            string message = "Zona Segura";
            string textColour = "246 246 246 255"; ;

            if (inPvpZone)
            {
                message = "Zona PVP";
                textColour = "1.0 0.3 0.3 1.0";
            }

            CuiHelper.DestroyUi(player, "PvpStatus");
            CuiElementContainer container = new CuiElementContainer();

            // Crea un elemento de panel que contendrá el texto
            container.Add(new CuiPanel
            {
                RectTransform = {
                    AnchorMin = "0 0.9",
                    AnchorMax = "0.1 1",
                    OffsetMin = "10 20",
                    OffsetMax = "-10 -10"
                },
                Image = {
                    Color = "0.1 0.1 0.1 0.95"
                }
            }, "Hud", "PvpStatus");

            // Agrega un elemento de texto al panel
            container.Add(new CuiLabel
            {
                RectTransform = { AnchorMin = "0 0", AnchorMax = "1 1" },
                Text = { Text = message, FontSize = 18, Align = TextAnchor.MiddleCenter, Color = textColour }
            }, "PvpStatus");

            CuiHelper.AddUi(player, container);
        }

    }
}
