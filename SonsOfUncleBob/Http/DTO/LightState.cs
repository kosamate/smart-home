using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Http.DTO
{
    //Valamiért nem tudtuk a projekt dependency-jébe a Server projektet felvenni
    //Így egyelőre itt is megvalósítottam ezeket az osztályokat/struktúrát, hogy a http klienssel tudjak haladni
    public readonly struct LightState
    {
        public const bool Off = false;
        public const bool On = true;
    }
}
