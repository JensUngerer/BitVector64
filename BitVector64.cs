using System;

namespace JensUngerer.BitVector64
{
    /// <summary>
    /// A BitVector with a 64 bit storage.
    /// It is based on: https://referencesource.microsoft.com/#system/compmod/system/collections/specialized/bitvector32.cs
    /// and http://www.dotnetframework.org/default.aspx/4@0/4@0/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/fx/src/CompMod/System/Collections/Specialized/BitVector32@cs/1305376/BitVector32@cs.
    /// </summary>
    public struct BitVector64
    {
        public const int NUMBER_OF_BITS_IN_BITVECTOR64 = 64;

        public ulong Data { get; private set; }

        public BitVector64(ulong data)
        {
            Data = data;
        }

        public BitVector64(BitVector64 value)
        {
            Data = value.Data;
        }

        public bool this[long bit] {
            get {
                var castedBit = (ulong) bit;
                return (Data & castedBit) == castedBit;
            }
            set {
                var castedBit = (ulong) bit;
                if (value) {
                    Data |= castedBit;
                }
                else {
                    Data &= ~castedBit;
                }
            }
        }

        public static long[] createMasks() {
            var masks = new long[NUMBER_OF_BITS_IN_BITVECTOR64];
            masks[0] = CreateMask();
            for (int i = 1; i < NUMBER_OF_BITS_IN_BITVECTOR64; i++)
            {
                masks[i] = CreateMask(masks[i-1]);
            }
            return masks;
        }

        public static long CreateMask() {
            return CreateMask(0);
        }

        public static long CreateMask(long previous) {
            if (previous == 0) {
                return 1;
            }
 
            if (previous == unchecked((long)0x8000000000000000)) {
                // SR.GetString(SR.BitVectorFull)
                throw new InvalidOperationException();
            }
 
            return previous << 1;
        }

        public override bool Equals(object o) {
            if (!(o is BitVector64)) {
                return false;
            }
            
            return Data == ((BitVector64)o).Data;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
