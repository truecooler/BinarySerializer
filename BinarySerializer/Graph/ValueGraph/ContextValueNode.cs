﻿using System.IO;
using BinarySerialization.Graph.TypeGraph;

namespace BinarySerialization.Graph.ValueGraph
{
    internal class ContextValueNode : ValueNode
    {
        public ContextValueNode(Node parent, string name, TypeNode typeNode) : base(parent, name, typeNode)
        {
        }

        public ValueNode Child { get; private set; }

        public override object Value
        {
            get
            {
                return Child.Value;
            }

            set
            {
                Child = ((RootTypeNode)TypeNode).Child.CreateSerializer(this);
                Child.Value = value;
            }
        }

        public object Context
        {
            set
            {
                if (value == null)
                    return;

                /* We have to dynamically generate a type graph for this new type */
                var contextGraph = new RootTypeNode(value.GetType());
                var contextSerializer = (ContextValueNode)contextGraph.CreateSerializer(this);
                contextSerializer.Value = value;

                Children.AddRange(contextSerializer.Child.Children);
            }
        }

        public override void Bind()
        {
            Child.Bind();
        }

        protected override void SerializeOverride(Stream stream, EventShuttle eventShuttle)
        {
            Child.Serialize(stream, eventShuttle);
        }

        public override void DeserializeOverride(StreamLimiter stream, EventShuttle eventShuttle)
        {
            Child = ((RootTypeNode)TypeNode).Child.CreateSerializer(this);
            Child.Deserialize(stream, eventShuttle);
        }
    }
}