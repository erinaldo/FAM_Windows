﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baran.Ferroalloy.Automation.Models;


namespace Baran.Ferroalloy.Automation
{
    public class UnitOfWork : IDisposable
    {
        dbAfrzEntities db = new dbAfrzEntities();
        private IPartTypes _partTypes;
        private ICategories _categories;
        private IStore _stores;
        private IPartName _partName;
        private IPartBranch _partBranch;
        private IPartSubBranch _partSubBranch;
        private IMeasurementUnits _measurementUnits;
        private IProperties _properties;
        private IShift _shift;
        private IDepartment _department;
        private ISubDepartment _subDepartment;
        private IMaintenanceItem _maintenanceItem;
        private IMaintenanceParts _maintenanceParts;
        private IEmployees _employees;
        private IMaintenance _maintenance;
        private IZone _zone;
        private ISubZone _subZone;
        private IEquipName _equipName;
        private IEquipSamples _equipSamples;
        private ICompanies _companies;
        private ILocations _locations;


        public IPartTypes PartTypes
        {
            get
            {
                if (_partTypes == null)
                {
                    _partTypes = new PartTypesService(db);
                }
                return _partTypes;
            }
        }

        public ICategories Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories=new Categories(db);
                }

                return _categories;
            }
        }

        public IStore Stores
        {
            get
            {
                if (_stores == null)
                {
                    _stores=new Store(db);
                }

                return _stores;
            }
        }

        public IPartName PartName
        {
            get
            {
                if (_partName == null)
                {
                    _partName = new PartNameService(db);
                }

                return _partName;
            }
        }

        public IPartBranch PartBranch
        {
            get
            {
                if (_partBranch == null)
                {
                    _partBranch=new PartBranchService(db);
                }

                return _partBranch;
            }
        }

        public IPartSubBranch PartSubBranch
        {
            get
            {
                if (_partSubBranch == null)
                {
                    _partSubBranch=new PartSubBranchService(db);
                }

                return _partSubBranch;
            }
        }

        public IMeasurementUnits MeasurementUnits
        {
            get
            {
                if (_measurementUnits == null)
                {
                    _measurementUnits=new MeasurementUnits(db);
                }

                return _measurementUnits;
            }
        }

        public IProperties properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties=new Properties(db);
                }

                return _properties;
            }
        }

        public IShift shift
        {
            get
            {
                if (_shift == null)
                {
                    _shift=new Shift(db);
                }

                return _shift;
            }
        }

        public IDepartment department
        {
            get
            {
                if (_department == null)
                {
                    _department=new DepartmentService(db);
                }

                return _department;
            }
        }

        public ISubDepartment subDepartment
        {
            get
            {
                if (_subDepartment == null)
                {
                    _subDepartment=new SubDepartmentService(db);
                }

                return _subDepartment;
            }
        }

        public IMaintenanceItem MaintenanceItem
        {
            get
            {
                if (_maintenanceItem == null)
                {
                    _maintenanceItem=new MaintenanceItemsService(db);
                }

                return _maintenanceItem;
            }
        }

        public IMaintenanceParts MaintenanceParts
        {
            get
            {
                if (_maintenanceParts == null)
                {
                    _maintenanceParts=new MaintenancePartsService(db);
                }

                return _maintenanceParts;
            }
        }

        public IEmployees Employees
        {
            get
            {
                if (_employees == null)
                {
                    _employees = new EmployeesService(db);
                }

                return _employees;
            }
            
        }

        public IMaintenance Maintenance
        {
            get
            {
                if (_maintenance == null)
                {
                    _maintenance=new MaintenanceService(db);
                }

                return _maintenance;
            }
        }

        public IZone Zone
        {
            get
            {
                if (_zone == null)
                {
                    _zone=new ZoneService(db);
                }

                return _zone;
            }
        }

        public ISubZone SubZone
        {
            get
            {
                if (_subZone == null)
                {
                    _subZone=new SubZoneService(db);
                }

                return _subZone;
            }
        }

        public IEquipName EquipName
        {
            get
            {
                if (_equipName == null)
                {
                    _equipName=new EquipNameService(db);
                }

                return _equipName;
            }
        }

        public IEquipSamples EquipSamples
        {
            get
            {
                if (_equipSamples == null)
                {
                    _equipSamples = new EquipSamplesService(db);
                }

                return _equipSamples;
            }
        }

        public ICompanies Companies
        {
            get
            {
                if (_companies == null)
                {
                    _companies=new CompaniesService(db);
                }

                return _companies;
            }
        }

        public ILocations Locations
        {
            get
            {
                if (_locations == null)
                {
                    _locations=new LocationsService(db);
                }

                return _locations;
            }
        }



        public void Dispose()
        {
            db.Dispose();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}