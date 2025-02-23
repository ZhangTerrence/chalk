type CourseResponse = {
  id: number;
  name: string;
  code: string | null;
  description: string | null;
  imageUrl: string | null;
  isPublic: boolean;
  createdOnUtc: string;
  updatedOnUtc: string;
};

export default CourseResponse;
