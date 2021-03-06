﻿namespace PerfDataService.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public sealed class StackViewerController
    {
        private readonly ICallTreeDataProvider dataProvider;

        public StackViewerController(ICallTreeDataProviderFactory dataProviderFactory)
        {
            if (dataProviderFactory == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(dataProviderFactory));
            }

            this.dataProvider = dataProviderFactory.Get();
        }

        [HttpGet]
        [Route("stackviewer/summary")]
        public List<TreeNode> Get(int numNodes, string find="")
        {
            return this.dataProvider.GetSummaryTree(numNodes, find);
        }

        [HttpGet]
        [Route("stackviewer/node")]
        public TreeNode Node(string name)
        {
            if (name == null) { return null; }
            return this.dataProvider.GetNode(name);
        }

        [HttpGet]
        [Route("stackviewer/callertree")]
        public TreeNode[] CallerTree(string name, string path="", string find="")
        {
            if (name == null) { return null; }
            return this.dataProvider.GetCallerTree(name, path, find);
        }

        [HttpGet]
        [Route("stackviewer/calleetree")]
        public TreeNode[] CalleeTree(string name, string path="", string find="")
        {
            if (name == null) { return null; }
            return this.dataProvider.GetCalleeTree(name, path, find);
        }

        [HttpGet]
        [Route("stackviewer/source")]
        public SourceInformation Source(string name)
        {
            if (name == null) { return null; }
            return this.dataProvider.Source(this.dataProvider.GetNode(name));
        }

        [HttpGet]
        [Route("stackviewer/source/caller")]
        public SourceInformation CallerContextSource(string name, string path="")
        {
            if (name == null) { return null; }
            return this.dataProvider.Source(this.dataProvider.GetCallerTreeNode(name, path));
        }

        [HttpGet]
        [Route("stackviewer/source/callee")]
        public SourceInformation CalleeContextSource(string name, string path="")
        {
            if (name == null) { return null; }
            return this.dataProvider.Source(this.dataProvider.GetCalleeTreeNode(name, path));
        }
    }
}