using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Models.FrameWork;
using PagedList;
using System.IO;
using System.Web;
using System.Data.Common;
using System.Configuration;


//dd

namespace Models
{
   public class AccountModel
    {
      
        // PPC_RentalEntities db = null;
        DemoPPCRentalEntities db = null;
        public AccountModel()
        {
            db = new DemoPPCRentalEntities();
        }
        public long Insert(USER entity)
        {
            db.USERs.Add(entity);
            db.SaveChangesAsync();
            return entity.ID;
        }
        public long InsertProperty(PROPERTy entity)
        {
            if (entity.ImageFile == null)
            {
                entity.Images = "ImagesNull.png";
            }
            if (entity.ImageFile2 == null)
            {
                entity.Avatar = "AvatarNull.png";
               
            }
            if (entity.District_ID ==null)
            {
                entity.District_ID = null ;
            }
            if (entity.Status_ID == null)
            {
                entity.District_ID = null;
            }


            db.PROPERTies.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var property = db.PROPERTies.Find(id);
                db.PROPERTies.Remove(property);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public PROPERTy ViewDetail(int id)
        {
            return db.PROPERTies.Find(id);
        }
        public bool Update(PROPERTy entitys)
        {
            try
            {
                var property = db.PROPERTies.Find(entitys.ID);

                property.PropertyName = entitys.PropertyName;
               // save images
                if (entitys.ImageFile == null)
                {
                    //entitys.Images = "ImagesNull.png";
                }
                else
                {
                    property.Images = entitys.Images;
                }
                // save avatar
                if (entitys.ImageFile2 == null)
                {
                    //entitys.Avatar = "AvatarNull.png";
                }
                else
                {
                    property.Avatar = entitys.Avatar;
                }

                //property.Avatar = entitys.Avatar;
                //property.Images = entitys.Images;
                property.PropertyType_ID = entitys.PropertyType_ID;
                property.Content = entitys.Content;
                property.Street_ID = entitys.Street_ID;
                property.Ward_ID = entitys.Ward_ID;
                property.District_ID = entitys.District_ID;
                property.Price = entitys.Price;
                property.UnitPrice = entitys.UnitPrice;
                property.Area = entitys.Area;
                property.BedRoom = entitys.BedRoom;
                property.BathRoom = entitys.BathRoom;
                property.PackingPlace = entitys.PackingPlace;
                property.UserID = entitys.UserID;
                //property.Created_at = DateTime.Parse(DateTime.Now.ToString("yyyy-mm-dd"));
                //property.Create_post = DateTime.Parse(DateTime.Now.ToString("yyyy-mm-dd"));
                property.Status_ID = entitys.Status_ID;
                property.Note = entitys.Note;
                //property.Updated_at = DateTime.Parse(DateTime.Now.ToString("yyyy-mm-dd"));
                property.Updated_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                property.Sale_ID = entitys.Sale_ID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
             
        }
        public IEnumerable<PROPERTy> ListAllPaging(int page, int pageSize)
        {
            return db.PROPERTies.OrderByDescending(x=> x.Created_at).ToPagedList(page, pageSize);
        }
        public IEnumerable<PROPERTy> ListAllPagingHome(int page, int pageSize)
        {
            return db.PROPERTies.Where(x=>x.Status_ID == 3).OrderByDescending(x => x.Created_at).ToPagedList(page, pageSize);
        }
        public IEnumerable<USER> ListAllPagingUser(int page, int pageSize)
        {
            return db.USERs.OrderByDescending(x => x.FullName).ToPagedList(page, pageSize);
        }
        //public IEnumerable<PROPERTy> ListAllAgency(int page, int pageSize)
        //{
        //    return db.PROPERTies.Where(x=>x.UserID==id).ToPagedList(page, pageSize);
        //}
        public USER GetID(string userName)
        {
            return db.USERs.SingleOrDefault(x => x.Email == userName);

        }
        public List<string> GetListCredentials(string userName)
        {
            var user = db.USERs.Single(x => x.Email == userName);

            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                       join c in db.Roles on a.RoleID equals c.ID
                       where b.ID ==user.GroupID
                       select new 
                       {
                          RoleID = a.RoleID,
                          UserGroupID = a.UserGroupID
                       }).AsEnumerable().Select(x=> new Credential() {
                           RoleID = x.RoleID,
                           UserGroupID = x.UserGroupID
                       });
            return data.Select(x => x.RoleID).ToList();
            
        }
      
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {

            // sing or find
            var res = db.USERs.SingleOrDefault(x => x.Email == userName);

            if (res == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (res.GroupID.Equals(CommonConstants.SALE_GROUP) || res.GroupID == CommonConstants.AGENCY_GROUP)
                    {
                        if (res.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (res.Password == passWord)
                           
                                
                                return 1;
                          

                            else
                                return -2;

                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (res.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (res.Password == passWord)
                            return 1;
                        else
                            return -2;

                    }
                }


            }




        }
    }
}
