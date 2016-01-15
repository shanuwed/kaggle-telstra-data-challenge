﻿using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TelReducer_USQL
{

    /*
    5 Severity_type (st)
    53 Event_type (et_1 through et_53)
    386 LogFeatures (lf_1 through lf_386)
    10 ResType (rt_1 through rt_10)
    1126 Location (loc)
    */
    enum ColNames
    {
        lf_1, lf_2, lf_3, lf_4, lf_5, lf_6, lf_7, lf_8, lf_9, lf_10, lf_11, lf_12, lf_13, lf_14, lf_15, lf_16, lf_17, lf_18, lf_19, lf_20, lf_21, lf_22, lf_23, lf_24, lf_25, lf_26, lf_27, lf_28, lf_29, lf_30, lf_31, lf_32, lf_33, lf_34, lf_35, lf_36, lf_37, lf_38, lf_39, lf_40, lf_41, lf_42, lf_43, lf_44, lf_45, lf_46, lf_47, lf_48, lf_49, lf_50, lf_51, lf_52, lf_53, lf_54, lf_55, lf_56, lf_57, lf_58, lf_59, lf_60, lf_61, lf_62, lf_63, lf_64, lf_65, lf_66, lf_67, lf_68, lf_69, lf_70, lf_71, lf_72, lf_73, lf_74, lf_75, lf_76, lf_77, lf_78, lf_79, lf_80, lf_81, lf_82, lf_83, lf_84, lf_85, lf_86, lf_87, lf_88, lf_89, lf_90, lf_91, lf_92, lf_93, lf_94, lf_95, lf_96, lf_97, lf_98, lf_99, lf_100, lf_101, lf_102, lf_103, lf_104, lf_105, lf_106, lf_107, lf_108, lf_109, lf_110, lf_111, lf_112, lf_113, lf_114, lf_115, lf_116, lf_117, lf_118, lf_119, lf_120, lf_121, lf_122, lf_123, lf_124, lf_125, lf_126, lf_127, lf_128, lf_129, lf_130, lf_131, lf_132, lf_133, lf_134, lf_135, lf_136, lf_137, lf_138, lf_139, lf_140, lf_141, lf_142, lf_143, lf_144, lf_145, lf_146, lf_147, lf_148, lf_149, lf_150, lf_151, lf_152, lf_153, lf_154, lf_155, lf_156, lf_157, lf_158, lf_159, lf_160, lf_161, lf_162, lf_163, lf_164, lf_165, lf_166, lf_167, lf_168, lf_169, lf_170, lf_171, lf_172, lf_173, lf_174, lf_175, lf_176, lf_177, lf_178, lf_179, lf_180, lf_181, lf_182, lf_183, lf_184, lf_185, lf_186, lf_187, lf_188, lf_189, lf_190, lf_191, lf_192, lf_193, lf_194, lf_195, lf_196, lf_197, lf_198, lf_199, lf_200, lf_201, lf_202, lf_203, lf_204, lf_205, lf_206, lf_207, lf_208, lf_209, lf_210, lf_211, lf_212, lf_213, lf_214, lf_215, lf_216, lf_217, lf_218, lf_219, lf_220, lf_221, lf_222, lf_223, lf_224, lf_225, lf_226, lf_227, lf_228, lf_229, lf_230, lf_231, lf_232, lf_233, lf_234, lf_235, lf_236, lf_237, lf_238, lf_239, lf_240, lf_241, lf_242, lf_243, lf_244, lf_245, lf_246, lf_247, lf_248, lf_249, lf_250, lf_251, lf_252, lf_253, lf_254, lf_255, lf_256, lf_257, lf_258, lf_259, lf_260, lf_261, lf_262, lf_263, lf_264, lf_265, lf_266, lf_267, lf_268, lf_269, lf_270, lf_271, lf_272, lf_273, lf_274, lf_275, lf_276, lf_277, lf_278, lf_279, lf_280, lf_281, lf_282, lf_283, lf_284, lf_285, lf_286, lf_287, lf_288, lf_289, lf_290, lf_291, lf_292, lf_293, lf_294, lf_295, lf_296, lf_297, lf_298, lf_299, lf_300, lf_301, lf_302, lf_303, lf_304, lf_305, lf_306, lf_307, lf_308, lf_309, lf_310, lf_311, lf_312, lf_313, lf_314, lf_315, lf_316, lf_317, lf_318, lf_319, lf_320, lf_321, lf_322, lf_323, lf_324, lf_325, lf_326, lf_327, lf_328, lf_329, lf_330, lf_331, lf_332, lf_333, lf_334, lf_335, lf_336, lf_337, lf_338, lf_339, lf_340, lf_341, lf_342, lf_343, lf_344, lf_345, lf_346, lf_347, lf_348, lf_349, lf_350, lf_351, lf_352, lf_353, lf_354, lf_355, lf_356, lf_357, lf_358, lf_359, lf_360, lf_361, lf_362, lf_363, lf_364, lf_365, lf_366, lf_367, lf_368, lf_369, lf_370, lf_371, lf_372, lf_373, lf_374, lf_375, lf_376, lf_377, lf_378, lf_379, lf_380, lf_381, lf_382, lf_383, lf_384, lf_385, lf_386,
        et_1, et_2, et_3, et_4, et_5, et_6, et_7, et_8, et_9, et_10, et_11, et_12, et_13, et_14, et_15, et_16, et_17, et_18, et_19, et_20, et_21, et_22, et_23, et_24, et_25, et_26, et_27, et_28, et_29, et_30, et_31, et_32, et_33, et_34, et_35, et_36, et_37, et_38, et_39, et_40, et_41, et_42, et_43, et_44, et_45, et_46, et_47, et_48, et_49, et_50, et_51, et_52, et_53,
        rt_1, rt_2, rt_3, rt_4, rt_5, rt_6, rt_7, rt_8, rt_9, rt_10,
        id, loc, fs, tr, st
    }


    /// <summary> 
    /// For more information about writing UDOs, please refer to http://go.microsoft.com/fwlink/?LinkID=623598&clcid=0x409 
    /// </summary> 
    [SqlUserDefinedReducer]
    public class TelReducer : IReducer
    {
        string[] colNames = Enum.GetNames(typeof(ColNames));

        Lookup eventType = new Lookup((int)ColNames.et_1);
        Lookup logFeature = new Lookup((int)ColNames.lf_1);
        Lookup resType = new Lookup((int)ColNames.rt_1);
        Lookup sevType = new Lookup(1);
        Lookup location = new Lookup(1);

        public TelReducer(string eventTypeInputFile, string logFeatureInputFile, string resTypeInputFile, string sevTypeInputFile, string locInputFile)
        {
            eventType.Load(Path.GetFileName(eventTypeInputFile));
            logFeature.Load(Path.GetFileName(logFeatureInputFile));
            resType.Load(Path.GetFileName(resTypeInputFile));
            sevType.Load(Path.GetFileName(sevTypeInputFile));
            location.Load(Path.GetFileName(locInputFile));
        }

        /// <summary> 
        ///  
        /// </summary> 
        /// <param name="input"></param> 
        /// <param name="output"></param> 
        /// <returns></returns> 
        public override IEnumerable<IRow> Reduce(IRowset input, IUpdatableRow output)
        {
            int count = 0;
            int[] colValues = new int[colNames.Length];

            foreach (IRow row in input.Rows)
            {
                if (count == 0)
                {
                    colValues[(int)ColNames.id] = int.Parse(row.Get<string>("id").ToString());
                    colValues[(int)ColNames.loc] = location.GetValue(row.Get<string>("loc").ToString());
                    colValues[(int)ColNames.fs] = int.Parse(row.Get<string>("fs").ToString());
                    colValues[(int)ColNames.tr] = int.Parse(row.Get<string>("tr").ToString());
                    colValues[(int)ColNames.st] = sevType.GetValue(row.Get<string>("st").ToString());
                }

                colValues[eventType.GetValue(row.Get<string>("et").ToString())] = 1;
                int vol = int.Parse(row.Get<string>("vol").ToString());
                colValues[logFeature.GetValue(row.Get<string>("lf").ToString())] = vol;
                colValues[resType.GetValue(row.Get<string>("rt").ToString())] = 1;

                count++;
            }

            // Write output
            for (int n = (int)ColNames.lf_1; n < colValues.Length; n++)
            {
                string colName = colNames[n];
                output.Set(colName, colValues[n].ToString());
            }
            yield return output.AsReadOnly();
        }
    }


    public class LookupWrapper
    {
        static Lookup lookup;

        public static Lookup Initialize(string filename, int columnOffset)
        {
            return lookup;
        }
    }

    public class Lookup
    {
        readonly int columnOffsetIndex = 0;

        Dictionary<string, int> lookup = null;

        Object _syncRoot = new object();
        bool _isInitialized = false;

        public Lookup(int columnOffset)
        {
            this.columnOffsetIndex = columnOffset;
        }

        public void Load(string filename)
        {
            if (!_isInitialized)
            {
                lock (_syncRoot)
                {
                    if (!_isInitialized)
                    {
                        Init(filename);
                        _isInitialized = true;
                    }
                }
            }
        }

        void Init(string filename)
        {
            lookup = new Dictionary<string, int>();

            // BUGBUG: this is a temporary workaround to run U-SQL locally since "DEPLOY RESOURCE" doesn't seem to be working correct.
            if (!File.Exists(filename))
            {
                filename = Path.Combine(@"D:\work\GitHub\kaggle-telstra-data-challenge\TelReducer_USQL\TelReducer_USQL", filename);
                if (!File.Exists(filename))
                    throw new FileNotFoundException("File is still not found: " + filename);
            }

            using (StreamReader sr = new StreamReader(filename))
            {
                int rownum = 1;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (!line.Contains("id"))
                    {
                        string[] tokens = line.Split(new char[] { ',' });
                        if (tokens.Length == 1)
                        {
                            //int indexnum = rownum - 1 + (int)ColNames.fn_1;
                            int index = rownum - 1 + columnOffsetIndex;
                            lookup.Add(tokens[0], index);
                        }
                        else if (tokens.Length == 2)
                        {
                            int index = rownum - 1 + columnOffsetIndex;
                            lookup.Add(tokens[0] + tokens[1], index);
                        }
                        else
                        {
                            throw new System.IndexOutOfRangeException("Bad number of tokens: " + tokens.Length);
                        }
                        rownum++;
                    }
                }
            }
        }

        public int GetValue(string input)
        {
            string key = input.Trim(new char[] { '"' });
            if (lookup.ContainsKey(key))
                return lookup[key];
            else
                throw new KeyNotFoundException("Key not found: " + key);
            //return -1; // if not found it means key is not in top x
        }
    }
}