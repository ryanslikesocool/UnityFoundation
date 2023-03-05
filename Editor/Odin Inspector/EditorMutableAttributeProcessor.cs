#if ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Foundation.Editors {
    internal sealed class EditorMutableAttributeProcessor<Value> : OdinAttributeProcessor<EditorMutable<Value>> {
        private static readonly Type[] allowedCopyAttributes = new Type[] {
            typeof(AssetListAttribute),
            typeof(AssetSelectorAttribute),
            typeof(ChildGameObjectsOnlyAttribute),
            typeof(ColorPaletteAttribute),
            typeof(EnumPagingAttribute),
            typeof(EnumToggleButtonsAttribute),
            typeof(FilePathAttribute),
            typeof(FolderPathAttribute),
            typeof(HideMonoScriptAttribute),
            typeof(InlineEditorAttribute),
            typeof(MultiLinePropertyAttribute),
            typeof(PreviewFieldAttribute),
            typeof(SceneObjectsOnlyAttribute),
            typeof(TableListAttribute),
            typeof(TableMatrixAttribute),
            typeof(ToggleLeftAttribute),
            typeof(AssetsOnlyAttribute),
            typeof(CustomValueDrawerAttribute),
            typeof(DelayedPropertyAttribute),
            typeof(EnableGUIAttribute),
            typeof(GUIColorAttribute),
            typeof(RequiredAttribute),
            typeof(SearchableAttribute),
            typeof(ShowInInspectorAttribute),
            typeof(TypeFilterAttribute),
            typeof(ValidateInputAttribute),
            typeof(ValueDropdownAttribute),
            typeof(DisallowModificationsInAttribute),
            typeof(DrawWithUnityAttribute),
            typeof(HideDuplicateReferenceBoxAttribute),
            typeof(OnCollectionChangedAttribute),
            typeof(OnValueChangedAttribute),
            typeof(DictionaryDrawerSettings),
            typeof(ListDrawerSettingsAttribute),
            typeof(TableColumnWidthAttribute),
            typeof(MinValueAttribute),
            typeof(MaxValueAttribute),
            typeof(MinMaxSliderAttribute),
            typeof(ProgressBarAttribute),
            typeof(WrapAttribute),
            typeof(PropertyRangeAttribute),
            typeof(RangeAttribute),
        };

        private Attribute[] superAttributes = default;

        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes) {
            superAttributes = attributes.ToArray();
            attributes.Add(new InlinePropertyAttribute());
        }

        public override void ProcessChildMemberAttributes(
            InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes
        ) {
            // These attributes will be added to all of the child elements.
            attributes.Add(new HideLabelAttribute());

            foreach (Attribute attr in superAttributes) {
                if (allowedCopyAttributes.Contains(v => attr.GetType() == v)) {
                    attributes.Add(attr);
                }
            }

            // Here we add attributes to child properties respectively.
            //if (member.Name == "_value") {
            //    attributes.Add(new EnumToggleButtonsAttribute());
            //} else if (member.Name == "Size") {
            //    attributes.Add(new RangeAttribute(0, 5));
            //}
        }
    }
}
#endif