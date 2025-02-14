using System;
using System.Collections.Generic;

namespace DocGen.Abstract.Constants
{
    /// <summary>
    /// Provides default numbering types for titles and allows 
    /// overriding the type per hierarchy level.
    /// </summary>
    public class TitleNumbering
    {
        public List<TitleNumberingType> DefaultNumberingTypes { get; private set; }

        public TitleNumbering()
        {
            DefaultNumberingTypes = new List<TitleNumberingType>
            {
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric,
                TitleNumberingType.Numeric
            };
        }

        public void SetNumberingType(int level, TitleNumberingType type)
        {
            if (level < 1 || level > DefaultNumberingTypes.Count)
                throw new ArgumentOutOfRangeException(Messages.TitleLevelIntervalErrorMessages);

            DefaultNumberingTypes[level - 1] = type;
        }

        public TitleNumberingType GetNumberingType(int level)
        {
            if (level < 1 || level > DefaultNumberingTypes.Count)
                throw new ArgumentOutOfRangeException(Messages.TitleLevelIntervalErrorMessages);

            return DefaultNumberingTypes[level - 1];
        }
    }
}
