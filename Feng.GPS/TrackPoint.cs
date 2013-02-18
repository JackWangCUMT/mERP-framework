using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    public class GpsData : IEntity
    {
        #region  Properties
        ///<summary>
        /// Id
        ///</summary>
        [Id(0, Name = "ID", Column = "Id")]
        [Generator(1, Class = "identity")]
        public virtual long ID
        {
            get;
            set;
        }

        ///<summary>
        /// ���ƺ�
        ///</summary>
        [Property(Length = 20, NotNull = true)]
        public virtual string VehicleName
        {
            get;
            set;
        }


        ///<summary>
        /// GPS�е�ʱ�䡣UTCʱ�䣬�й��Ƕ�8��
        ///</summary>
        [Property(NotNull = true)]
        public virtual DateTime GpsTime
        {
            get;
            set;
        }

        ///<summary>
        /// ����
        ///</summary>
        [Property(NotNull = true)]
        public virtual double Longitude
        {
            get;
            set;
        }

        ///<summary>
        /// γ��
        ///</summary>
        [Property(NotNull = true)]
        public virtual double Latitude
        {
            get;
            set;
        }

        ///<summary>
        /// �ٶ�
        ///</summary>
        [Property(NotNull = true)]
        public virtual double Speed
        {
            get;
            set;
        }

        ///<summary>
        /// ����
        ///</summary>
        [Property(NotNull = true)]
        public virtual double Heading
        {
            get;
            set;
        }

        /// <summary>
        /// ��ȷ�ȣ���λΪ�ף�
        /// </summary>
        [Property(NotNull = true)]
        public virtual double Accuracy
        {
            get;
            set;
        }

        /// <summary>
        /// �߶�
        /// </summary>
        [Property(NotNull = true)]
        public virtual double Altitude
        {
            get;
            set;
        }

        ///// <summary>
        ///// STATUS_WORD
        ///// </summary>
        //[Property(Column = "STATUS_WORD", NotNull = false)]
        //public virtual int StatusWord
        //{
        //    get;
        //    set;
        //}

        /////<summary>
        ///// DATA_STATE
        /////</summary>
        //[Property(Column = "DATA_STATE", Length = 4, NotNull = false)]
        //public virtual string DataState
        //{
        //    get;
        //    set;
        //}

        ///<summary>
        /// ��Ϣ����ʱ��
        ///</summary>
        [Property(NotNull = true)]
        public virtual DateTime MessageTime
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual bool IsActive
        {
            get;
            set;
        }
        //public virtual DateTime LocalGpsTime
        //{
        //    get { return this.GpsTime.AddHours(8); }
        //}
        #endregion
    }


    [Class(Table = "SD_Track_Point", OptimisticLock = OptimisticLockMode.Version)]
    public class TrackPoint : GpsData
    {
        [ManyToOne(ForeignKey = "FK_TrackPoint_Track", NotNull = false)]
        public virtual Track Track
        {
            get;
            set;
        }

		//#region "search Result"
        /////<summary>
        ///// RoadName��·����
        /////</summary>
        //[Property(Column = "ROAD_NAME", Length = 50, NotNull = false)]
        //public virtual string RoadName
        //{
        //    get;
        //    set;
        //}

        /////<summary>
        ///// SiteName��������
        /////</summary>
        //[Property(Column = "SITE_NAME", Length = 50, NotNull = false)]
        //public virtual string SiteName
        //{
        //    get;
        //    set;
        //}

        /////<summary>
        ///// �ȴ�ʱ��
        /////</summary>
        //[Property(Column = "WAIT_TIME", NotNull = false)]
        //public virtual int? WaitTime
        //{
        //    get;
        //    set;
        //}
		//#endregion

		//#region "Methods"
        //private static CoordSys m_coordSys = Session.Current.CoordSysFactory.CreateLongLat(DatumID.WGS84);
        ///// <summary>
        ///// WGS84 ��γ������ϵ
        ///// </summary>
        //public static CoordSys DefaultCoordSys
        //{
        //    get
        //    {
        //        return m_coordSys;
        //    }
        //}

        ///// <summary>
        ///// ��ø������ Mapinfo Point
        ///// </summary>
        //public MapInfo.Geometry.Point Point
        //{
        //    get
        //    {
        //        return new MapInfo.Geometry.Point(m_coordSys, new DPoint(m_LONGITUDE, m_LATITUDE));
        //    }
        //}

        //public object Clone()
        //{
        //    GPSData data = new GPSData();
        //    data.VEHICLE_ID = this.VEHICLE_ID;
        //    data.DATA_STATE = this.DATA_STATE;
        //    data.GPS_TIME = this.GPS_TIME;
        //    data.LONGITUDE = this.LONGITUDE;
        //    data.LATITUDE = this.LATITUDE;
        //    data.SPEED = this.SPEED;
        //    data.DIRECTION = this.DIRECTION;
        //    data.STATUS_WORD = this.STATUS_WORD;
        //    data.MESSAGE_TIME = this.MESSAGE_TIME;

        //    return data;
        //}
		//#endregion
	}
}
