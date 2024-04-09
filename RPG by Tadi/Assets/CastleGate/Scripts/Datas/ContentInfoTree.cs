using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Tadi.Datas.UI;
using Tadi.Utils.Tree;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Tadi.UI.ScrollView
{
    public enum ItemState
    {
        Origin,
        Deactivated
    }

    public enum MenuType
    {
        None,
        UnitMenu,
        ActionMenu,
        TargetMenu,
        SkillMenu,
        SkillTargetMenu
    }

    public class ContentInfoTree
    {
        // Menu Tree
        private TreeNode<ContentInfo> root; // Assume a Tree is constructed
        private Tree<ContentInfo> tree;
        //private int curDepthIndex = 0;
        //private int curBreadthIndex = 0;
        private int itemCountPerPage;

        public ContentInfoTree(int itemCountPerPage)
        {
            this.itemCountPerPage = itemCountPerPage;
        }

        public void SetRootMenu(MenuType type, List<ItemInfo> itemInfoList)
        {
            root?.RemoveAllNodes();
            root = new TreeNode<ContentInfo>(new ContentInfo(type, itemInfoList, itemCountPerPage)); // Assume a Tree is constructed
            tree = new Tree<ContentInfo>(root);
        }

        public void AddChildMenus(int parentDepthIndex, MenuType type, List<ItemInfo> itemInfoList)
        {
            int parentBreadthCount = tree.GetBreadthAtDepth(parentDepthIndex);

            for (int i = 0; i < parentBreadthCount; i++)
            {
                TreeNode<ContentInfo> parent = root.FindNodeByIndices(parentDepthIndex, i);

                for (int j = 0; j < parent.value.ItemInfoList.Count; j++) 
                {
                    if (parent.value.ItemInfoList[j].ChildType == type)
                    {
                        TreeNode<ContentInfo> child = new TreeNode<ContentInfo>(new ContentInfo(type, itemInfoList, itemCountPerPage));
                        parent.AddChild(child);
                    }
                }
            }
        }

        public void AddChildMenus(int parentDepthIndex, MenuType type, List<List<ItemInfo>> itemInfoList)
        {
            int parentBreadth = tree.GetBreadthAtDepth(parentDepthIndex);

            for (int i = 0; i < parentBreadth; i++)
            {
                TreeNode<ContentInfo> parent = root.FindNodeByIndices(parentDepthIndex, i);

                for (int j = 0; j < parent.value.ItemInfoList.Count; j++)
                {
                    if (parent.value.ItemInfoList[j].ChildType == type)
                    {
                        TreeNode<ContentInfo> child = new TreeNode<ContentInfo>(new ContentInfo(type, itemInfoList[i], itemCountPerPage));
                        parent.AddChild(child);
                    }
                }
            }
        }

        public ContentInfo GetContentInfo(int depthIndex, int breadthIndex)
        {
            TreeNode<ContentInfo> node = root.FindNodeByIndices(depthIndex, breadthIndex);

            return node.value;
        }

        public ContentInfo GetContentInfo(MenuType type, int depthIndex, int unitIndex)
        {
            List<ContentInfo> contentInfoList = tree.GetListAtDepth(depthIndex);
            ContentInfo contentInfo = contentInfoList.Where(c => c.Type == type).ToList()[unitIndex];

            return contentInfo;
        }

        public void SetItemState(MenuType type, int depthIndex, int unitIndex)
        {
            List<ContentInfo> contentInfoList = tree.GetListAtDepth(depthIndex);
            ContentInfo contentInfo = contentInfoList.Where(c => c.Type == type).ToList()[unitIndex];
        }

        public void ResetAllMenu()
        {
            // Perform in-order traversal
            List<ContentInfo> menusInfo = tree.InOrderTraversal();

            foreach (ContentInfo menuInfo in menusInfo)
            {
                menuInfo.PrevItemIndex = 0;
                menuInfo.CurItemIndex = 0;

                foreach (ItemInfo itemInfo in menuInfo.ItemInfoList)
                {
                    itemInfo.ColorState = ItemState.Origin;
                }
            }
        }

    }

    public class ContentInfo
    {
        public MenuType Type { get; set; }
        public List<ItemInfo> ItemInfoList { get; set; }
        public int PrevItemIndex { get; set; } = 0;
        public int CurItemIndex { get; set; } = 0;
        public int PrevItemPage
        {
            get { return (PrevItemIndex / ItemCountPerPage) + 1; }
            private set { PrevItemPage = value; }
        }
        public int CurItemPage
        {
            get { return (CurItemIndex / ItemCountPerPage) + 1; }
            private set { CurItemPage = value; }
        }
        public int ItemCountPerPage { get; private set; }
        public bool IsChangedPage { get { return PrevItemPage != CurItemPage; } }

        public ContentInfo(MenuType type, List<ItemInfo> itemInfoList, int itemCountPerPage)
        {
            Type = type;
            ItemInfoList = itemInfoList;
            PrevItemIndex = 0;
            CurItemIndex = 0;
            ItemCountPerPage = itemCountPerPage;
        }
    }

    public class ItemInfo
    {
        public string Name { get; set; }
        public Color TextColor { get; set; } = UIData.OriginColor;
        public ItemState ColorState { get; set; } = ItemState.Origin;
        public MenuType ChildType { get; set; }

        public ItemInfo(string name, MenuType childType)
        {
            Name = name;
            ChildType = childType;
        }
    }
}
