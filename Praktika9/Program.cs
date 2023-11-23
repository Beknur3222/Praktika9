using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika9
{
    abstract class Storage
    {
        protected string name;
        protected string model;

        public Storage(string name, string model)
        {
            this.name = name;
            this.model = model;
        }

        public abstract double GetMemory();

        public abstract void CopyData(double dataSize);

        public abstract double GetFreeMemory();

        public abstract string GetDeviceInfo();
    }

    class Flash : Storage
    {
        private double usbSpeed;
        private double memorySize;

        public Flash(string name, string model, double usbSpeed, double memorySize)
            : base(name, model)
        {
            this.usbSpeed = usbSpeed;
            this.memorySize = memorySize;
        }

        public override double GetMemory()
        {
            return memorySize;
        }

        public override void CopyData(double dataSize)
        {
            Console.WriteLine($"Copying {dataSize} GB to Flash drive");
        }

        public override double GetFreeMemory()
        {
            return memorySize;
        }

        public override string GetDeviceInfo()
        {
            return $"Flash Drive: {name} - {model}, USB Speed: {usbSpeed} GB/s, Memory Size: {memorySize} GB";
        }
    }

    class DVD : Storage
    {
        private double readWriteSpeed;
        private string type;

        public DVD(string name, string model, double readWriteSpeed, string type)
            : base(name, model)
        {
            this.readWriteSpeed = readWriteSpeed;
            this.type = type;
        }

        public override double GetMemory()
        {
            return type == "односторонний" ? 4.7 : 9;
        }

        public override void CopyData(double dataSize)
        {
            Console.WriteLine($"Copying {dataSize} GB to DVD");
        }

        public override double GetFreeMemory()
        {
            return GetMemory();
        }

        public override string GetDeviceInfo()
        {
            return $"DVD Disk: {name} - {model}, Read/Write Speed: {readWriteSpeed} GB/s, Type: {type}, Memory Size: {GetMemory()} GB";
        }
    }

    class HDD : Storage
    {
        private double usbSpeed;
        private int partitionCount;
        private double partitionSize;

        public HDD(string name, string model, double usbSpeed, int partitionCount, double partitionSize)
            : base(name, model)
        {
            this.usbSpeed = usbSpeed;
            this.partitionCount = partitionCount;
            this.partitionSize = partitionSize;
        }

        public override double GetMemory()
        {
            return partitionCount * partitionSize;
        }

        public override void CopyData(double dataSize)
        {
            Console.WriteLine($"Copying {dataSize} GB to Removable HDD");
        }

        public override double GetFreeMemory()
        {
            return GetMemory();
        }

        public override string GetDeviceInfo()
        {
            return $"Removable HDD: {name} - {model}, USB Speed: {usbSpeed} GB/s, Partitions: {partitionCount}, Partition Size: {partitionSize} GB, Memory Size: {GetMemory()} GB";
        }
    }

    class Program
    {
        static void Main()
        {
            Storage[] devices = new Storage[]
            {
            new Flash("Flash1", "Model1", 3.0, 64),
            new DVD("DVD1", "Model2", 2.0, "односторонний"),
            new HDD("HDD1", "Model3", 2.0, 2, 500)
            };

            //1
            double totalMemory = 0;
            foreach (var device in devices)
            {
                totalMemory += device.GetMemory();
            }

            Console.WriteLine($"Total Memory of all devices: {totalMemory} GB");

            //2-3 
            double dataSize = 565; //GB
            foreach (var device in devices)
            {
                device.CopyData(dataSize);
            }

            //4
            double dataToTransfer = 780; //MB
            int requiredDevices = (int)Math.Ceiling(dataSize / dataToTransfer);

            Console.WriteLine($"Required devices for transfer: {requiredDevices}");

            Console.ReadKey();
        }
    }

}
